using Application.Common.Behaviours;
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

namespace Application.Users.Commands.Insert
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, long>
    {
        private readonly IGalleryDbContext _context;

        public InsertUserCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;
            byte[] passwordHash;
            byte[] passwordSalt;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            command.Username = command.Username.ToLower();

            if (String.IsNullOrEmpty(command.FirstName) || String.IsNullOrWhiteSpace(command.FirstName))
                command.FirstName = null;

            if (String.IsNullOrEmpty(command.LastName) || String.IsNullOrWhiteSpace(command.LastName))
                command.LastName = null;

            bool exists = await _context.Users.AnyAsync(x => x.Username.Equals(command.Username));

            if (exists)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);

            Hasher.CreatePasswordHash(command.Password, out passwordHash, out passwordSalt);

            User entity = new User()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Username = command.Username,
                RoleId = command.RoleId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
