using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>
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
            var userByUserName = await _userManager.FindByEmailAsync(request.Email);
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
            //  Mapping
            var newUser = _mapper.Map(request, oldUser);
            //  Update
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
            //  Return Messsage
            return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
        }
        #endregion
    }
}
