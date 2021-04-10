using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contacts.Commands.Delete
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
    {
        private readonly IGalleryDbContext _context;

        public DeleteContactCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
        {
            if (command.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);

            Contact entity = await _context.Contacts.FindAsync(command.Id);

            if (entity == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.ContactNotFound);

            _context.Contacts.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
