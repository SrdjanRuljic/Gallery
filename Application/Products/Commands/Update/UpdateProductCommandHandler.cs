using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IGalleryMySqlDbContext _context;
        private readonly IBlogService _blogService;

        public UpdateProductCommandHandler(IGalleryMySqlDbContext context,
                                           IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!request.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            Product entity = await _context.Products.FindAsync(request.Id);

            entity.CategoryId = request.CategoryId;
            entity.Content = request.Content;
            entity.Description = request.Description;
            entity.Extension = request.Extension;
            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            await _blogService.SaveSingleAsync(entity);

            return true;
        }
    }
}
