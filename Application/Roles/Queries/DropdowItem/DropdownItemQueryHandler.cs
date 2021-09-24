using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Roles.Queries.DropdowItem
{
    public class DropdownItemQueryHandler : IRequestHandler<DropdownItemQuery, List<DropdownItemViewModel>>
    {
        private readonly IManagersServices _managersServices;
        private readonly IMapper _mapper;

        public DropdownItemQueryHandler(IManagersServices managersServices,
                                        IMapper mapper)
        {
            _managersServices = managersServices;
            _mapper = mapper;
        }

        public async Task<List<DropdownItemViewModel>> Handle(DropdownItemQuery request, CancellationToken cancellationToken)
        {
            List<DropdownItemViewModel> list = await _managersServices.GetAllRoles()
                                                                      .ProjectTo<DropdownItemViewModel>(_mapper.ConfigurationProvider)
                                                                      .ToListAsync();

            return list;
        }
    }
}
