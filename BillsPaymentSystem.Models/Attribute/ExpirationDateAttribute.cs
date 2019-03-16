using System;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.Models.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExpirationDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime currentDate = DateTime.Now;

            if (currentDate > (DateTime)value)
            {
                return new ValidationResult("Card is expired!");
            }

            return  ValidationResult.Success;

        }
    }
}