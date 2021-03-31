using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IGalleryDbContext context,
                                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCategoryByIdViewModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);


            GetCategoryByIdViewModel model = await _context.Categories
                                                           .ProjectTo<GetCategoryByIdViewModel>(_mapper.ConfigurationProvider)
                                                           .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (model == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);

            return model;
        }
    }
}
