using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolProject.Service.AuthService.Interfaces;

namespace SchoolProject.Core.Filters
{
    public class AuthFilter : IAsyncActionFilter
    {
        #region Fields
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructors
        public AuthFilter(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        #endregion

        #region Functions
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var roles = await _currentUserService.GetRolesAsync();
                if (roles.All(x => x != "User"))
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                else
                {
                    await next();
                }
            }
        }
        #endregion

    }
}
