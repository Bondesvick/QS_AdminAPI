using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QuickServiceAdmin.Core.Helpers
{
    public class Numeric : ValidationAttribute

    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var inputValue = (string) value;

            return inputValue.Any(c => int.TryParse(c.ToString(), out var intValue) == false)
                ? new ValidationResult("The field " + validationContext.MemberName +
                                       " must contain only numeric characters")
                : ValidationResult.Success;
        }
    }
}