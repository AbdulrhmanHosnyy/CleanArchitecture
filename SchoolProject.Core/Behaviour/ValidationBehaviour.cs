using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Behaviour
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        #region Fields
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _validators = validators;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Functions
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var message = failures.Select(x => _stringLocalizer[$"{x.PropertyName}"] + " : " + _stringLocalizer[x.ErrorMessage]).FirstOrDefault();

                    throw new ValidationException(message);

                }
            }
            return await next();
        }
        #endregion
    }

}
