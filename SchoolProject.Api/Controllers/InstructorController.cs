using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Data.AppMetaData;


namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpGet(Router.InstructortRouting.GetInstructorSalarySummation)]
        public async Task<IActionResult> GetInstructorSalarySummation()
        {
            return NewResult(await Mediator.Send(new GetInstructorSalarySummationQuery()));
        }
        [HttpPost(Router.InstructortRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddInstructorCommand addInstructorCommand)
        {
            return NewResult(await Mediator.Send(addInstructorCommand));
        }
    }
}
