using Application.Products.Queries.Elasticsearch;
using Application.Products.Queries.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        #region [POST]

        [HttpPost]
        [Route("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(SearchProductsQuery query)
        {
            SearchProductsViewModel result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Route("elasticsearch")]
        [AllowAnonymous]
        public async Task<IActionResult> Elasticsearch(ElasticsearchProductQuery query)
        {
            ElasticsearchProductViewModel result = await Mediator.Send(query);

            return Ok(result);
        }

        #endregion [POST]
    }
}
