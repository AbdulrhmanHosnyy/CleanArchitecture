﻿using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Users.Commands.Validations
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public AddUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Functions
        public void ApplyValidationRules()
        {
            RuleFor(u => u.FullName)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                 .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(u => u.UserName)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                 .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(u => u.Password)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(u => u.ConfirmPassword)
               .Equal(u => u.Password).WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotEqualConfirmPass]);
        }
        public void ApplyCustomValidationRules()
        {

        }
        #endregion
    }
}
