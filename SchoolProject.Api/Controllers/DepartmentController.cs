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

        #endregion
    }
}
