using Application.Categories.Commands.InsertCategoryCommand;
using Application.Categories.Commands.UpdateCategoryCommand;
using Application.Categories.Queries;
using Application.Categories.Queries.GetAllCategories;
using Application.Categories.Queries.GetCategoryById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        #region [GET]

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            GetCategoryByIdViewModel model = await Mediator.Send(new GetCategoryByIdQuery()
            {
                Id = id
            });

            return Ok(model);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            List<GetAllCategoriesViewModel> list = await Mediator.Send(new GetAllCategoriesQuery() { });

            return Ok(list);
        }

        [HttpGet]
        [Route("dropdown")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDropdownItems()
        {
            List<DropdownItemViewModel> items = await Mediator.Send(new DropdownItemQuery());

            return Ok(items);
        }

        #endregion

        #region [POST]

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(InsertCategoryCommand model)
        {
            long id = await Mediator.Send(model);

            return Ok(id);
        }

        #endregion [POST]

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UpdateCategoryCommand model)
        {
            bool isUpdated = await Mediator.Send(model);

            return Ok(isUpdated);
        }

        #endregion
    }
}
