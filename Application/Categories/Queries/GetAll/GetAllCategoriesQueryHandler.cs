using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<GetAllCategoriesViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IGalleryDbContext context,
                                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetAllCategoriesViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            List<GetAllCategoriesViewModel> list = await _context.Categories
                                                                 .ProjectTo<GetAllCategoriesViewModel>(_mapper.ConfigurationProvider)
                                                                 .ToListAsync();

            return list;
        }
    }
}
