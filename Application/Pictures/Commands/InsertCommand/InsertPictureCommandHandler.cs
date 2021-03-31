using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pictures.Commands.InsertCommand
{
    public class InsertPictureCommandHandler : IRequestHandler<InsertPictureCommand, long>
    {
        private readonly IGalleryDbContext _context;

        public InsertPictureCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(InsertPictureCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            Picture entity = new Picture()
            {
                CategoryId = command.CategoryId,
                Content = command.Content,
                Description = command.Description,
                Extension = command.Extension,
                Name = command.Name
            };

            _context.Pictures.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
