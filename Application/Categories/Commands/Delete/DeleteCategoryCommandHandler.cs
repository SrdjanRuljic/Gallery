using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IGalleryDbContext _context;

        public DeleteCategoryCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            if (command.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);

            bool exists = await _context.Pictures.AnyAsync(x => x.CategoryId == command.Id);

            if (exists)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CanNotDeleteCategory);

            Category entity = await _context.Categories.FindAsync(command.Id);

            if (entity == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);

            _context.Categories.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
