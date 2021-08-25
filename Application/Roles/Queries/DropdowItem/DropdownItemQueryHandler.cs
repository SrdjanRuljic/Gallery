using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Queries.DropdowItem
{
    public class DropdownItemQueryHandler : IRequestHandler<DropdownItemQuery, List<DropdownItemViewModel>>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public DropdownItemQueryHandler(RoleManager<Role> roleManager,
                                        IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<List<DropdownItemViewModel>> Handle(DropdownItemQuery request, CancellationToken cancellationToken)
        {
            List<DropdownItemViewModel> list = await _roleManager.Roles
                                                                 .ProjectTo<DropdownItemViewModel>(_mapper.ConfigurationProvider)
                                                                 .ToListAsync();

            return list;
        }
    }
}
