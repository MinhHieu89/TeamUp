using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TeamUp.Core.Models
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParseExact(
                Convert.ToString(value),
                "MM/dd/yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None, out dateTime);

            if (!isValid || dateTime >= DateTime.Now)
                return new ValidationResult("Ngày sinh không chính xác");

            return ValidationResult.Success;
        }
    }
}
