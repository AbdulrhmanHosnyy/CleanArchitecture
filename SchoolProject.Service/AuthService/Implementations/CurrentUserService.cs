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
        private readonly HttpContextAccessor _contextAccessor;
        #endregion

        #region Constructors
        public CurrentUserService(UserManager<User> userManager, HttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        #endregion

        #region Functions
        public int GetUserId()
        {
            var usedId = _contextAccessor.HttpContext.User.Claims
                .SingleOrDefault(claim => claim.Type == nameof(UserClaims.Id)).Value;
            if (usedId is null) throw new UnauthorizedAccessException();
            return int.Parse(usedId);
        }
        public async Task<User> GetUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) throw new UnauthorizedAccessException();
            return user;
        }

        public async Task<List<string>> GetRolesAsync()
        {
            var user = await GetUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        #endregion
    }
}
