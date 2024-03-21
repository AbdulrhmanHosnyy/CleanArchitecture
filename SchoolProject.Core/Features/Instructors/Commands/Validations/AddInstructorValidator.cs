using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructors.Commands.Validations
{
    public class AddInstructorValidator : AbstractValidator<AddInstructorCommand>
    {

        #region Fields
        private readonly IInstructorService _instructorService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructors
        public AddInstructorValidator(IStringLocalizer<SharedResources> stringLocalizer, IInstructorService instructorService, IDepartmentService departmentService)
        {
            _stringLocalizer = stringLocalizer;
            _instructorService = instructorService;
            _departmentService = departmentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(s => s.NameAr)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                 .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(s => s.NameEn)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                 .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(s => s.DID)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(s => s.NameAr)
                .MustAsync(async (Key, CancellationToken) => !await _instructorService.IsNameArExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);

            RuleFor(s => s.NameEn)
                .MustAsync(async (Key, CancellationToken) => !await _instructorService.IsNameEnExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);

            RuleFor(s => s.DID)
                .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsNotExist]);
        }
        #endregion
    }
}
