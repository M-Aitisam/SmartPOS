using System;
using System.ComponentModel.DataAnnotations;

namespace ClassLibraryEntities
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
            ErrorMessage = $"You must be at least {minimumAge} years old.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Now)
                {
                    return new ValidationResult("Date of birth cannot be in the future.");
                }

                var age = DateTime.Now.Year - dateValue.Year;
                if (dateValue > DateTime.Now.AddYears(-age)) age--;

                if (age < _minimumAge)
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date");
        }
    }
}