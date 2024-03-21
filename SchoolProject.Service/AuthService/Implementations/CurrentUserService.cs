using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.AuthService.Interfaces;

namespace SchoolProject.Service.AuthService.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        #region Constructors
        public CurrentUserService(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        #endregion

        #region Functions
        public int GetUserId()
        {
            var userId = _contextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserClaims.Id)).Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(userId);
        }

        public async Task<User> GetUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            { throw new UnauthorizedAccessException(); }
            return user;
        }

        public async Task<List<string>> GetCurrentUserRolesAsync()
        {
            var user = await GetUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        #endregion
    }
}
