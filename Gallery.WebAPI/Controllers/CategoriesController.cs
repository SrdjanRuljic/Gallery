using AutoMapper;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryBusiness categoryBusiness, IMapper mapper)
        {
            _categoryBusiness = categoryBusiness;
            _mapper = mapper;
        }

        #region [POST]

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(CategoryViewModel model)
        {
            long id = 0;

            try
            {
                id = await _categoryBusiness.Insert(_mapper.Map<CategoryModel>(model));
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        #endregion [POST]

        #region [GET]

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CategoryViewModel> list = _mapper.Map<List<CategoryViewModel>>(await _categoryBusiness.GetAll());

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                CategoryViewModel model = _mapper.Map<CategoryViewModel>(await _categoryBusiness.GetById(id));

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        [Route("category-exists/{name}/{id}")]
        public async Task<IActionResult> Exists(string name, long id)
        {
            bool exists = await _categoryBusiness.Exists(name, id);

            return Ok(exists);
        }

        [HttpGet]
        [Route("dropdown")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDropdownItems()
        {
            List<DropdownItemViewModel> items = _mapper.Map<List<DropdownItemViewModel>>(await _categoryBusiness.GetDropdownItems());

            return Ok(items);
        }

        #endregion

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(CategoryViewModel model)
        {
            bool isUpdated = false;

            try
            {
                await _categoryBusiness.Update(_mapper.Map<CategoryModel>(model));

                return Ok(isUpdated);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        #endregion

        #region [DELETE]

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _categoryBusiness.Delete(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        #endregion
    }
}