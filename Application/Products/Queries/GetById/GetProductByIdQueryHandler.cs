using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Nest;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdViewModel>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IElasticClient elasticClient,
                                          IMapper mapper)
        {
            _elasticClient = elasticClient;
            _mapper = mapper;
        }

        public async Task<GetProductByIdViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);

            ISearchResponse<Product> response = await _elasticClient.SearchAsync<Product>(s =>
                s.Query(q =>
                    q.QueryString(q =>
                        q.Fields(f =>
                            f.Field(f =>
                                f.Id)).Query(request.Id.ToString()))));

            GetProductByIdViewModel viewModel = _mapper.Map<GetProductByIdViewModel>(response.Documents.FirstOrDefault());

            if (viewModel == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.DataNotFound);

            return viewModel;
        }
    }
}
