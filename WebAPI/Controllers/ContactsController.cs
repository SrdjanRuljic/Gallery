using Application.Contacts.Commands.Insert;
using Application.Contacts.Commands.Update;
using Application.Contacts.Queries.GetAll;
using Application.Contacts.Queries.GetById;
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
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            GetContactByIdViewModel model = await Mediator.Send(new GetContactByIdQuery
            {
                Id = id
            });

            return Ok(model);
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            List<GetAllContactsViewModel> list = await Mediator.Send(new GetAllContactsQuery());

            return Ok(list);
        }

        #endregion

        #region [PUT]

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UpdateContactCommand model)
        {
            bool isUpdated = await Mediator.Send(model);

            return Ok(isUpdated);
        }

        #endregion

        #region [POST]

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(InsertContactCommand model)
        {
            long id = await Mediator.Send(model);

            return Ok(id);
        }

        #endregion [POST]
    }
}
