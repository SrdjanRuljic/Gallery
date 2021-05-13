using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IGalleryDbContext _context;

        public UpdateUserCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!request.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            User entity = await _context.Users.FindAsync(request.Id);

            request.Username = request.Username.ToLower();

            if (String.IsNullOrEmpty(request.FirstName) || String.IsNullOrWhiteSpace(request.FirstName))
                request.FirstName = null;

            if (String.IsNullOrEmpty(request.LastName) || String.IsNullOrWhiteSpace(request.LastName))
                request.LastName = null;

            bool exists = await _context.Users.AnyAsync(x => x.Username.Equals(request.Username) &&
                                                             x.Id != request.Id);

            if (exists)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.CategoryExists);

            entity.Username = request.Username;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.RoleId = request.RoleId;

            await _context.SaveChangesAsync(cancellationToken);


            return true;
        }
    }
}
