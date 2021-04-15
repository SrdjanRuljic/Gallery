using Application.Common.Helpers;
using MediatR;

namespace Application.Products.Queries.Elasticsearch
{
    public class ElasticsearchProductQuery : PaginateModel, IRequest<ElasticsearchProductViewModel>
    {
        public string Name { get; set; }
    }
}
