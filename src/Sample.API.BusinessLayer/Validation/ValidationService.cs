using FluentValidation;

namespace Sample.API.BusinessLayer.Validation;

public static class ValidationService
{
    public static async Task ValidationEntity<T>(T instance, IValidator<T> validator) where T : class
    {
        var validationResult = await validator.ValidateAsync(instance);

        if (!validationResult.IsValid)
        {
            throw new ValidationException("UnprocessableEntity", validationResult.Errors);
        }
    }
}