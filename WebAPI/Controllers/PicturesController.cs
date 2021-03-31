﻿using Application.Pictures.Commands.Insert;
using Application.Pictures.Commands.Search;
using Application.Pictures.Queries.GetDetailsById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : BaseController
    {
        #region [GET]

        [HttpGet]
        [Route("single/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleById(long id)
        {
            GetDetailsByIdViewModel model = await Mediator.Send(new GetDetailsByIdQuery
            {
                Id = id
            });

            return Ok(model);
        }

        #endregion

        #region [POST]

        [HttpPost, DisableRequestSizeLimit]
        [Route("")]
        public async Task<IActionResult> Insert(InsertPictureCommand model)
        {
            long id = await Mediator.Send(model);

            return Ok(id);
        }

        [HttpPost]
        [Route("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(SearchPicturesCommand command)
        {
            List<SearchPicturesCommandViewModel> pictures = await Mediator.Send(command);

            return Ok(pictures);
        }

        #endregion [POST]
    }
}
