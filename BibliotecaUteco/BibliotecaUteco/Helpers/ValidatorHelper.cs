
using FluentValidation.Results;

namespace BibliotecaUteco.Helpers;

public static class ValidatorHelper
{
    public static bool ValidateRequest(ValidationResult result)
    {
        if (!result.IsValid) throw new ValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        
        return true;    
    }
}