using Application.Products.Commands.Insert;
using Application.Products.Queries.Elasticsearch;
using Application.Products.Queries.GetById;
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
        #region [GET]

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long id)
        {
            GetProductByIdViewModel picture = await Mediator.Send(new GetProductByIdQuery
            {
                Id = id
            });

            return Ok(picture);
        }

        #endregion

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

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> Insert(InsertProductCommand model)
        {
            long id = await Mediator.Send(model);

            return Ok(id);
        }

        #endregion [POST]
    }
}
