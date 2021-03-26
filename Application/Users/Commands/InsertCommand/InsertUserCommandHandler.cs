using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, long>
    {
        private readonly IGalleryDbContext _context;

        public InsertUserCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(InsertUserCommand model, CancellationToken cancellationToken)
        {
            string errorMessage = null;
            byte[] passwordHash;
            byte[] passwordSalt;

            if (!model.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            model.Username = model.Username.ToLower();

            if (String.IsNullOrEmpty(model.FirstName) || String.IsNullOrWhiteSpace(model.FirstName))
                model.FirstName = null;

            if (String.IsNullOrEmpty(model.LastName) || String.IsNullOrWhiteSpace(model.LastName))
                model.LastName = null;

            bool exists = await _context.Users.AnyAsync(x => x.Username.Equals(model.Username));

            if(exists)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);

            Hasher.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            User entity = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                RoleId = model.RoleId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
