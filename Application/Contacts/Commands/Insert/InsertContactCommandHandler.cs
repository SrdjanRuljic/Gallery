using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contacts.Commands.Insert
{
    public class InsertContactCommandHandler : IRequestHandler<InsertContactCommand, long>
    {
        private readonly IGalleryDbContext _context;

        public InsertContactCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(InsertContactCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);


            Contact entity = new Contact()
            {
                Name = command.Name,
                Value = command.Value
            };

            _context.Contacts.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
