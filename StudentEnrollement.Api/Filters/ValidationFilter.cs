
using FluentValidation;
using StudentEnrollement.Api.DTOs.Authentication;

namespace StudentEnrollement.Api.Filters
{
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        private readonly IValidator<T> _validator;
        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //Run Before endponts code
            var objectToValite = context.GetArgument<T>(0);

            var validationResult = await _validator.ValidateAsync(objectToValite);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }
            var result = await next(context);

            //Something after endpoint code

            return result;
        }
    }
}
