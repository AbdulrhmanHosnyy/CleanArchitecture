using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailsService _emailsService;
        private readonly AppDbContext _appDbContext;
        private readonly IUrlHelper _urlHelper;
        #endregion

        #region Constructors
        public UserService(UserManager<User> userManager, IHttpContextAccessor contextAccessor,
            IEmailsService emailsService, AppDbContext appDbContext, IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _emailsService = emailsService;
            _appDbContext = appDbContext;
            _urlHelper = urlHelper;
        }
        #endregion

        #region Functions
        public async Task<string> AddUserAsync(User user, string password)
        {
            var transition = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                //  Cheack email existance
                var userByEmail = await _userManager.FindByEmailAsync(user.Email);
                if (userByEmail is not null) return "EmailIsExist";

                //  Cheack username existance
                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                if (userByUserName is not null) return "UserNameIsExist";

                //  Create User
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded) return string.Join(",", result.Errors.Select(e => e.Description).ToList());

                //  Add User Role
                await _userManager.AddToRoleAsync(user, "User");

                //  Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _contextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host +
                    _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var message = $"To confirm email click link <a href='{returnUrl}'></a>";
                //$"/api/V1/Authentication/ConfirmEmail?userId{user.Id}&code={code}";

                await _emailsService.SendEmail(user.Email, message, "Confirm Email");
                await transition.CommitAsync();
                return "Success";
            }
            catch
            {
                await transition.RollbackAsync();
                return "Failed";
            }

        }
        #endregion

    }
}
