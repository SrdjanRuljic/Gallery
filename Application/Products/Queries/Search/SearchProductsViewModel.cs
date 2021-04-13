using Application.Common.Helpers;
using System.Collections.Generic;

namespace Application.Products.Queries.Search
{
    public class SearchProductsViewModel : PaginateResultModel
    {
        public List<SearchProductsQueryResult> Products { get; set; }
    }
}
