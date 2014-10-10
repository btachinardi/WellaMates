using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WellaMates.Filters
{
    public class ValidateDateStringAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dtout;
            if (DateTime.TryParse(value.ToString(), out dtout))
            {
                return true;
            }
            return false;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dtout;
            if (DateTime.TryParse(value.ToString(), out dtout))
            {
                return null;
            }
            return new ValidationResult(FormatErrorMessage("A data não é válida."));
        }
    }
}