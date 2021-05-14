using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IGalleryDbContext _context;

        public DeleteUserCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);

            User entity = await _context.Users.FindAsync(request.Id);

            if (entity == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);

            _context.Users.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
