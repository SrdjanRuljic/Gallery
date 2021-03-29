using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Queries
{
    public class DropdownItemQueryHandler : IRequestHandler<DropdownItemQuery, List<DropdownItemViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public DropdownItemQueryHandler(IGalleryDbContext context,
                                        IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DropdownItemViewModel>> Handle(DropdownItemQuery request, CancellationToken cancellationToken)
        {
            List<DropdownItemViewModel> list = await _context.Categories.ProjectTo<DropdownItemViewModel>(_mapper.ConfigurationProvider)
                                                                        .ToListAsync();

            return list;
        }
    }
}
