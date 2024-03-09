using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class StudentsController : AppControllerBase
    {
        #region Fields
        #endregion

        #region Consturctors
        #endregion

        #region Actions
        [HttpGet(Router.StudentRouting.List)]
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
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand addStudentCommand)
        {
            var response = await Mediator.Send(addStudentCommand);
            return NewResult(response);
        }
        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand editStudentCommand)
        {
            var response = await Mediator.Send(editStudentCommand);
            return NewResult(response);
        }
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return NewResult(await Mediator.Send(new DeleteStudentCommand(id)));
        }
        #endregion



    }
}
