using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Codes.Queries.GetParents
{
    public class GetParentsCodesQueryHandler : IRequestHandler<GetParentsCodesQuery, List<GetParentsCodesQueryViewModel>>
    {
        private readonly IGalleryMySqlDbContext _context;
        private readonly IMapper _mapper;

        public GetParentsCodesQueryHandler(IGalleryMySqlDbContext context,
                                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetParentsCodesQueryViewModel>> Handle(GetParentsCodesQuery request, CancellationToken cancellationToken)
        {
            List<GetParentsCodesQueryViewModel> list = await _context.CodesLanguages.Where(x =>
                x.LanguageId == request.LanguageId &&
                x.Code.ParentId == null).ProjectTo<GetParentsCodesQueryViewModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync();

            return list;
        }
    }
}
