using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Author.Commands.Update
{
    public class UpdateAboutAuthorCommandHandler : IRequestHandler<UpdateAboutAuthorCommand, bool>
    {
        private readonly IGalleryDbContext _context;

        public UpdateAboutAuthorCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateAboutAuthorCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            AboutAuthor entity = await _context.AboutAuthor.FindAsync(command.Id);

            entity.Content = command.Content;
            entity.Biography = command.Biography;
            entity.Extension = command.Extension;
            entity.Name = command.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
