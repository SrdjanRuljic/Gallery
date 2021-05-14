using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, bool>
    {
        private readonly IGalleryDbContext _context;

        public UpdatePasswordCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);

            Hasher.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            User entity = await _context.Users.FindAsync(request.Id);

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
