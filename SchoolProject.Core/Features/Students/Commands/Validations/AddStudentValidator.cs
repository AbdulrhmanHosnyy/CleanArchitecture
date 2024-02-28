using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #endregion

        #region Constructors
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> stringLocalizer, IDepartmentService departmentService)
        {
            _stringLocalizer = stringLocalizer;
            _studentService = studentService;
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

            RuleFor(s => s.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(s => s.DepartmentId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(s => s.NameAr)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameArExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);

            RuleFor(s => s.NameEn)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameEnExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);

            RuleFor(s => s.DepartmentId)
                .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsNotExist]);
        }
        #endregion

    }
}

