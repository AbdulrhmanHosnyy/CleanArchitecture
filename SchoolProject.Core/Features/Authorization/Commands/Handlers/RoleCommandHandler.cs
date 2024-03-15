﻿using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Functions
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success") return Success("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
            return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
            else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
            return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
        #endregion
    }
}
