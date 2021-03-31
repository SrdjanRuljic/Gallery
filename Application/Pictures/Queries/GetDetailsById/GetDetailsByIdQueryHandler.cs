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

namespace Application.Pictures.Queries.GetDetailsById
{
    public class GetDetailsByIdQueryHandler : IRequestHandler<GetDetailsByIdQuery, GetDetailsByIdViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetDetailsByIdQueryHandler(IGalleryDbContext context,
                                          IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetDetailsByIdViewModel> Handle(GetDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);


            GetDetailsByIdViewModel model = await _context.Pictures
                                                          .ProjectTo<GetDetailsByIdViewModel>(_mapper.ConfigurationProvider)
                                                          .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (model == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);

            return model;
        }
    }
}
