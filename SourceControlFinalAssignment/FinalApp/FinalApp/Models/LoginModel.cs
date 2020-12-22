using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FinalApp.CustValids;

namespace FinalApp.Models
{
    public class LoginModel
    {
        [Display(Name = "Email Address")]
        //[Required(ErrorMessage ="Please enter Email ")]
        //[EmailAddress(ErrorMessage ="Invalid Email Address Format")]
        //[RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
        public string EmailID { get; set; }

        [Display(Name = "Password")]
        //[Required(ErrorMessage ="Please enter Password")]
        //[MinLength(8, ErrorMessage = "Password must contain atleast 8 characters"), MaxLength(32, ErrorMessage = "Password can not contain more than 32 characters")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }
    }
}