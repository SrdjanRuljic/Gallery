using Application.Contacts.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : BaseController
    {
        #region [GET]

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            List<GetAllContactsViewModel> list = await Mediator.Send(new GetAllContactsQuery());

            return Ok(list);
        }

        #endregion
    }
}
