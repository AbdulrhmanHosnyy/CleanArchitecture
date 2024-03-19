using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Filters;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StudentsController : AppControllerBase
    {
        #region Fields
        #endregion

        #region Consturctors
        #endregion

        #region Actions
        [HttpGet(Router.StudentRouting.List)]
        [Authorize(Roles = "User")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetStudentsList()
        {
            var response = await Mediator.Send(new GetStudentsListQuery());
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet(Router.StudentRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            return NewResult(await Mediator.Send(new GetStudentdByIdQuery(id)));
        }
        [Authorize(Policy = "CreateStudent")]
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand addStudentCommand)
        {
            var response = await Mediator.Send(addStudentCommand);
            return NewResult(response);
        }
        [Authorize(Policy = "EditStudent")]
        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand editStudentCommand)
        {
            var response = await Mediator.Send(editStudentCommand);
            return NewResult(response);
        }
        [Authorize(Policy = "DeleteStudent")]
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return NewResult(await Mediator.Send(new DeleteStudentCommand(id)));
        }
        #endregion



    }
}
