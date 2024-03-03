using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Features.Users.Queries.Models;

using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpGet(Router.UserRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetUserById(int id)
        {
            return NewResult(await Mediator.Send(new GetUserByIdQuery(id)));
        }
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand addUserCommand)
        {
            var response = await Mediator.Send(addUserCommand);
            return NewResult(response);
        }
        [HttpPut(Router.UserRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand updateUserCommand)
        {
            var response = await Mediator.Send(updateUserCommand);
            return NewResult(response);
        }

    }
}
