using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Gallery.BLL;
using Gallery.WebAPI.Models;
using Microsoft.AspNetCore.Diagnostics;
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
            string message = null;
            IExceptionHandlerPathFeature exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                message = exceptionFeature.Error.Message;
            }

            return await Task.Run(() =>
            {
                return StatusCode(code, new ErrorDetailsViewModel()
                {
                    StatusCode = code,
                    Message = message == null ? ErrorMessages.Unauthorised : message
                });
            });
        }
    }
}
