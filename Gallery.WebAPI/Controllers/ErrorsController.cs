using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Gallery.BLL;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            return await Task.Run(() =>
            {
                return StatusCode(code, new ErrorDetailsViewModel()
                {
                    StatusCode = code,
                    Message = ErrorMessages.GetMessageForHttpCode(code)
                }); ;
            });
        }
    }
}
