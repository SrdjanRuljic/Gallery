using Application.Common.Helpers;
using System.Collections.Generic;

namespace Application.Products.Queries.Elasticsearch
{
    public class ElasticsearchProductViewModel : PaginateResultModel
    {
        public List<ElasticsearchProductQueryResult> Products { get; set; }
    }
}
