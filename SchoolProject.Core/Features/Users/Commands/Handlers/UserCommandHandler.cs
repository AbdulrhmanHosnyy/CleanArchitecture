using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

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
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper,
            UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //  Cheack email existance
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmail is not null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);

            //  Cheack username existance
            var userByUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userByUserName is not null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsExist]);

            //  Mapping user
            var identityUser = _mapper.Map<User>(request);

            //  Create User
            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Created("");
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
