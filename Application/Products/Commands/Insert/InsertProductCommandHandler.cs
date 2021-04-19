using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.Insert
{
    public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand, long>
    {
        private readonly IGalleryMySqlDbContext _context;
        private readonly IBlogService _blogService;

        public InsertProductCommandHandler(IGalleryMySqlDbContext context,
                                           IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }

        public async Task<long> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!request.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            Product entity = new Product()
            {
                CategoryId = request.CategoryId,
                Content = request.Content,
                Description = request.Description,
                Extension = request.Extension,
                Name = request.Name
            };

            _context.Products.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _blogService.SaveSingleAsync(entity);

            return entity.Id;
        }
    }
}
