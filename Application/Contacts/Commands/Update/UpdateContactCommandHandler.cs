using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contacts.Commands.Update
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, bool>
    {
        private readonly IGalleryDbContext _context;

        public UpdateContactCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateContactCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            Contact entity = await _context.Contacts.FindAsync(command.Id);

            if (command.Name != entity.Name || command.Value != entity.Value)
            {
                entity.Name = command.Name;
                entity.Value = command.Value;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}
