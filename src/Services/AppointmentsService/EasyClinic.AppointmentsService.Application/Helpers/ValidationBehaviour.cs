using FluentValidation;
using MediatR;
using FluentValidation.Results;
using EasyClinic.AppointmentsService.Domain.Exceptions;
using System.Text.Json;

namespace EasyClinic.AppointmentsService.Application.Helpers
{
    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => new 
                {
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage 
                })
                .ToList();

            if (errors.Any())
            {
                throw new ModelValidationException()
                { 
                    Errors = errors
                };
            }

            var response = await next();

            return response;
        }
    }
}
