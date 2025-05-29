using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Utilities.Attributes;
public class NumberValidationAttribute : ValidationAttribute
{
    private readonly string PropertyName;

    public NumberValidationAttribute(string propertyName)
    {
        PropertyName = propertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        List<string> invalidChars = new List<string>() { PropertyName };
        ErrorMessage = ErrorMessageString;

        if (!Regex.IsMatch(value.ToString(), @"\d{1,5}"))
        {
            return new ValidationResult(ErrorMessage, invalidChars);
        }
        return ValidationResult.Success;
    }
}
