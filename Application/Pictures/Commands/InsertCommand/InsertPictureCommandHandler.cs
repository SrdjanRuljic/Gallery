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

        public async Task<long> Handle(InsertPictureCommand model, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            Picture entity = new Picture()
            {
                CategoryId = model.CategoryId,
                Content = model.Content,
                Description = model.Description,
                Extension = model.Extension,
                Name = model.Name
            };

            _context.Pictures.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
