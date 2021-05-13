using Application.Roles.Queries.DropdowItem;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class RolesController : BaseController
    {
        #region [GET]

        [HttpGet]
        [Route("dropdown")]
        public async Task<IActionResult> GetDropdownItems()
        {
            List<DropdownItemViewModel> items = await Mediator.Send(new DropdownItemQuery());

            return Ok(items);
        }

        #endregion
    }
}
