using Application.Common.Helpers;
using MediatR;

namespace Application.Products.Queries.Search
{
    public class SearchProductsQuery : PaginateModel, IRequest<SearchProductsViewModel>
    {
        public string Name { get; set; }
    }
}
