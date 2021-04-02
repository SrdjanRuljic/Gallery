using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pictures.Commands.Update
{
    public class UpdatePictureCommandHandler : IRequestHandler<UpdatePictureCommand, bool>
    {
        private readonly IGalleryDbContext _context;

        public UpdatePictureCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdatePictureCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            Picture entity = await _context.Pictures.FindAsync(command.Id);

            entity.CategoryId = command.CategoryId;
            entity.Content = command.Content;
            entity.Description = command.Description;
            entity.Extension = command.Extension;
            entity.Name = command.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
