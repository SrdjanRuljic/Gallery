using AutoMapper;
using Gallery.BLL.Interfaces;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesBusiness _rolesBusiness;
        private readonly IMapper _mapper;
        public RolesController(IRolesBusiness rolesBusiness, IMapper mapper)
        {
            _rolesBusiness = rolesBusiness;
            _mapper = mapper;
        }

        #region [GET]

        [HttpGet]
        [Route("dropdown")]
        public async Task<IActionResult> GetDropdownItems()
        {
            List<DropdownItemViewModel> items = _mapper.Map<List<DropdownItemViewModel>>(await _rolesBusiness.GetDropdownItems());

            return Ok(items);
        }

        #endregion
    }
}