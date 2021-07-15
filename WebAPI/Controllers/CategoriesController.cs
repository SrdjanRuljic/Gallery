using Application.Categories.Commands.Delete;
using Application.Categories.Commands.Insert;
using Application.Categories.Commands.Update;
using Application.Categories.Queries;
using Application.Categories.Queries.GetAll;
using Application.Categories.Queries.GetById;
using Application.Common.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class CategoriesController : BaseController
    {
        #region [GET]

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            GetCategoryByIdViewModel model = await Mediator.Send(new GetCategoryByIdQuery
            {
                Id = id
            });

            return Ok(model);
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

        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAll(GetAllCategoriesQuery query)
        {
            PaginationResultViewModel<GetAllCategoriesViewModel> list = await Mediator.Send(query);

            return Ok(list);
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

        #region [DELETE]

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteCategoryCommand
            {
                Id = id
            });

            return Ok();
        }

        #endregion
    }
}
