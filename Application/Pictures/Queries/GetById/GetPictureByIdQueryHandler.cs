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

namespace Application.Pictures.Queries.GetById
{
    public class GetPictureByIdQueryHandler : IRequestHandler<GetPictureByIdQuery, GetPictureByIdViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetPictureByIdQueryHandler(IGalleryDbContext context,
                                          IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetPictureByIdViewModel> Handle(GetPictureByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);

            GetPictureByIdViewModel viewModel = await _context.Pictures
                                                              .ProjectTo<GetPictureByIdViewModel>(_mapper.ConfigurationProvider)
                                                              .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (viewModel == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);

            return viewModel;
        }
    }
}
