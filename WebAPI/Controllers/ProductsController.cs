using Application.Products.Queries.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            List<SearchProductsViewModel> products = await Mediator.Send(query);

            return Ok(products);
        }

        #endregion [POST]
    }
}
