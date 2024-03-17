using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class EmailsController : AppControllerBase
    {
        [HttpPost(Router.EmailsRouting.SendEmail)]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand sendEmailCommand)
        {
            var response = await Mediator.Send(sendEmailCommand);
            return NewResult(response);
        }
    }
}
