﻿using Application.Categories.Commands.InsertCategoryCommand;
using Application.Categories.Queries;
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
    }
}
