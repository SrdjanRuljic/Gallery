using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contacts.Queries.GetAll
{
    public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, List<GetAllContactsViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetAllContactsQueryHandler(IGalleryDbContext context,
                                          IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetAllContactsViewModel>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            List<GetAllContactsViewModel> list = await _context.Contacts
                                                               .ProjectTo<GetAllContactsViewModel>(_mapper.ConfigurationProvider)
                                                               .ToListAsync();

            return list;
        }
    }
}
