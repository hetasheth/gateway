using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ABS.WebCustomer.Models
{
    public class RegisterVM
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please Enter Address")]
        [StringLength(200, MinimumLength = 5)]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter City")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter State")]
        public string State { get; set; }

        [Display(Name = "Zipcode")]
        [Required(ErrorMessage = "Please Enter Zipcode")]
        [Range(111111, 999999, ErrorMessage = "Invalid Zipcode")]
        public string Zipcode { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address Format")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please Enter Mobile Number")]
        [Phone(ErrorMessage = "Incorrect Format")]
        [RegularExpression(@"^((\+)?(\d{2}[-]))?(\d{10}){1}?$", ErrorMessage = "Not a valid Mobile number")]
        public string MobileNo { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter Password")]
        [MinLength(8, ErrorMessage = "Password must contain atleast 8 characters"), MaxLength(32, ErrorMessage = "Password can not contain more than 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please Enter Password Again")]
        [MinLength(8, ErrorMessage = "Password must contain atleast 8 characters"), MaxLength(32, ErrorMessage = "Password can not contain more than 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        [Compare("Password", ErrorMessage = "Should be same as Password")]
        public string ConfPassword { get; set; }
    }
}