using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        #region Fields
        #endregion

        #region Consturctors
        #endregion

        #region Actions
        [HttpGet(Router.DepartmentRouting.GetById)]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery getDepartmentByIdQuery)
        {
            return NewResult(await Mediator.Send(getDepartmentByIdQuery));
        }
        [HttpGet(Router.DepartmentRouting.GetDepartmentStudentsCount)]
        public async Task<IActionResult> GetDepartmentStudentsCount()
        {
            return NewResult(await Mediator.Send(new GetDepartmentStudentCountQuery()));
        }
        [HttpGet(Router.DepartmentRouting.GetDepartmentStudentsCountById)]
        public async Task<IActionResult> GetDepartmentStudentsCountById([FromRoute] int DID)
        {
            return NewResult(await Mediator.Send(new GetDepartmentStudentCountByIdQuery(DID)));
        }

        #endregion
    }
}
