using Microsoft.AspNetCore.Mvc.ModelBinding;


public static class ModelStateValidation
{
    public static string GetFirstError(this ModelStateDictionary modelState)
    {
        string error = "";

        if (modelState.IsValid || modelState.Count <= 0)
            return error;

        return modelState.Values.First(x => x.ValidationState == ModelValidationState.Invalid).Errors.First().ErrorMessage;
    }
}

