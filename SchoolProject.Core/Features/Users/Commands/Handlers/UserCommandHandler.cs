using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailsService _emailsService;
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper,
            UserManager<User> userManager, IHttpContextAccessor contextAccessor, IEmailsService emailsService,
            IUserService userService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _emailsService = emailsService;
            _userService = userService;
        }
        #endregion

        #region Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //  Mapping user
            var identityUser = _mapper.Map<User>(request);

            //  Create User
            var result = await _userService.AddUserAsync(identityUser, request.Password);
            switch (result)
            {
                case "EmailIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
                case "UserNameIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsExist]);
                case "ErrorInCreateUser":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToAddUser]);
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success":
                    return Success<string>("");
                default:
                    return BadRequest<string>(result);
            }
        }
        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //  Check user existance
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser is null) return NotFound<string>();
            //  Cheack username existance
            var userByUserName = await _userManager.Users.AnyAsync(u => u.UserName == request.UserName &&
            u.Id != request.Id);
            if (userByUserName) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsExist]);
            //  Mapping
            var newUser = _mapper.Map(request, oldUser);
            //  Update
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
            //  Return messsage
            return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
        }
        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //  Check user existance
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null) return NotFound<string>();

            //  Removing user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeletedFailed]);
            //  Return message
            return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
        }
        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //  Check user existance
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null) return NotFound<string>();
            //  Chnage Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            //  Return massage
            return Success((string)_stringLocalizer[SharedResourcesKeys.Success]);

        }
        #endregion
    }
}
