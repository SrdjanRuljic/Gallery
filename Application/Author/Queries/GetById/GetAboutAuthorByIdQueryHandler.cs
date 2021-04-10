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

namespace Application.Author.Queries.GetById
{
    public class GetAboutAuthorByIdQueryHandler : IRequestHandler<GetAboutAuthorByIdQuery, GetAboutAuthorByIdViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetAboutAuthorByIdQueryHandler(IGalleryDbContext context,
                                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAboutAuthorByIdViewModel> Handle(GetAboutAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);


            GetAboutAuthorByIdViewModel model = await _context.AboutAuthor
                                                              .ProjectTo<GetAboutAuthorByIdViewModel>(_mapper.ConfigurationProvider)
                                                              .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (model == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.AuthorNotFound);

            return model;
        }
    }
}
