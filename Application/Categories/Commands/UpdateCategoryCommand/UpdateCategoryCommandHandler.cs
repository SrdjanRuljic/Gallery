using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IGalleryDbContext _context;

        public UpdateCategoryCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            Category entity = await _context.Categories.FindAsync(command.Id);

            bool exists = await _context.Categories.AnyAsync(x => x.Name.Equals(command.Name) &&
                                                                  x.Id != command.Id);

            if (exists)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.CategoryExists);

            if (command.Name != entity.Name)
            {
                entity.Name = command.Name;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}
