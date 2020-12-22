using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalApp.CustValids;

namespace FinalApp.Models
{
    public class RegisterModel
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Please Enter First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address Format")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
        public string EmailID { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [Phone(ErrorMessage ="Incorrect Format")]
        [RegularExpression(@"^((\+)?(\d{2}[-]))?(\d{10}){1}?$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Please Enter Date of Birth")]
        [DataType(DataType.Date)]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [AgeValid(ErrorMessage ="Age should be 18 or above")]
        public DateTime DOB { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Select Gender")]
        public string Gender { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please Enter Address")]
        [StringLength(200,MinimumLength =5)]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter City")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter State")]
        public string State { get; set; }

        [Display(Name = "Pincode")]
        [Required(ErrorMessage = "Please Enter Pincode")]
        [Range(111111, 999999, ErrorMessage = "Invalid Pincode")]
        public string Pincode { get; set; }
                
        [Display(Name = "Profile Photo")]
        [Required(ErrorMessage = "Please Upload Photo")]        
        public HttpPostedFileBase Photo { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Only image file can be uploaded")]
        public string imgPath {
            get
            {
                if (Photo != null)
                    return Photo.FileName;
                else
                    return "";
            }            
        }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter Password")]
        [MinLength(8, ErrorMessage = "Password must contain atleast 8 characters"), MaxLength(32, ErrorMessage = "Password can not contain more than 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please Enter Password Again")]
        [MinLength(8, ErrorMessage = "Password must contain atleast 8 characters"), MaxLength(32, ErrorMessage = "Password can not contain more than 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
        [Compare("Password",ErrorMessage ="Should be same as Password")]
        public string ConfPassword { get; set; }
    }
}