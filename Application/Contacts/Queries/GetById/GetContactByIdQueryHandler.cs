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

namespace Application.Contacts.Queries.GetById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, GetContactByIdViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetContactByIdQueryHandler(IGalleryDbContext context,
                                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetContactByIdViewModel> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);


            GetContactByIdViewModel model = await _context.Contacts
                                                          .ProjectTo<GetContactByIdViewModel>(_mapper.ConfigurationProvider)
                                                          .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (model == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.ContactNotFound);

            return model;
        }
    }
}
