using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands.InsertCategoryCommand
{
    public class InsertCategoryCommandHandler : IRequestHandler<InsertCategoryCommand, long>
    {
        private readonly IGalleryDbContext _context;

        public InsertCategoryCommandHandler(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(InsertCategoryCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            bool exists = await _context.Categories.AnyAsync(x => x.Name.Equals(command.Name));

            if (exists)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.CategoryExists);


            Category entity = new Category()
            {
                Name = command.Name
            };

            _context.Categories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
