using System;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.Models.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
    {
        private string targetProperty;

        public XorAttribute(string targgeProperty)
        {
            this.targetProperty = targgeProperty;

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetProperty = validationContext.ObjectType
                .GetProperty(this.targetProperty)
                .GetValue(validationContext.ObjectInstance);

            if ((value==null)^(targetProperty==null))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("The two properties must have opposite values!");



        }
    }
}