using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public EditStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
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
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(s => s.NameAr)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameArExistExcludeSelf(Key, model.Id))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);
            RuleFor(s => s.NameAr)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameEnExistExcludeSelf(Key, model.Id))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);
        }
        #endregion
    }
}
