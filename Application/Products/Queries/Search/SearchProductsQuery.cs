using MediatR;
using System.Collections.Generic;

namespace Application.Products.Queries.Search
{
    public class SearchProductsQuery : IRequest<List<SearchProductsViewModel>>
    {
        public string Name { get; set; }
    }
}
