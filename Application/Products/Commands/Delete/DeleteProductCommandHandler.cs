using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IGalleryMySqlDbContext _context;
        private readonly IBlogService _blogService;

        public DeleteProductCommandHandler(IGalleryMySqlDbContext context,
                                           IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);

            Product entity = await _context.Products.FindAsync(request.Id);

            if (entity == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.DataNotFound);

            _context.Products.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _blogService.DeleteAsync(entity);


            return Unit.Value;
        }
    }
}
