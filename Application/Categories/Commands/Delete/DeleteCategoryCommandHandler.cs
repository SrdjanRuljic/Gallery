using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System;
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
            string errorMessage = null;

            if (command.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);

            Category entity = await _context.Categories.FindAsync(command.Id);

            if (entity == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);


            try
            {
                _context.Categories.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
                    errorMessage = ErrorMessages.CanNotDeleteCategory;
                else
                    errorMessage = exception.Message;

                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            return Unit.Value;
        }
    }
}
