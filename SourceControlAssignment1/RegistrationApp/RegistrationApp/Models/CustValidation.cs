using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models
{
    public class CustValidation : ValidationAttribute
    {       

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value!=null)
            {
                string d = value.ToString();
                if(d.Equals("Trainee"))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid Designation!!!");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required...");
            }
        }
    }
}