using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models
{    
    public class Users
    {     
        public int userID { get; set; }

        [Required(ErrorMessage ="Please Enter Username")]    
        [StringLength(20,ErrorMessage ="Length can not be greater than 20")]
        [Display(Name ="Username")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Invalid Email Address")]
        [Display(Name ="Email Address")]
        public string emailID { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Invalid Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Display(Name ="Phone Number")]
        public int phoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [MinLength(6,ErrorMessage ="Password must be of atleast 6 characters"),MaxLength(32, ErrorMessage = "Password must be of atmost 32 characters")]
        [Display(Name ="Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Please Enter Date of Birth")]
        [RegularExpression(@"^(?:0[1-9]|[12]\d|3[01])([\/.-])(?:0[1-9]|1[012])\1(?:19|20)\d\d$",ErrorMessage ="Invalid format of date!!")]
        [Display(Name ="Date of Birth")]
        public string DOB { get; set; }

        [Required(ErrorMessage = "Please Enter SSC Percentage")]
        [Range(0,100,ErrorMessage ="Percentage must be between 0 to 100")]
        [Display(Name = "SSC Percentage")]
        public int perSSC { get; set; }

        [Required(ErrorMessage = "Please Enter HSC Percentage")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 to 100")]
        [Display(Name = "HSC Percentage")]
        public int perHSC { get; set; }

        [Required(ErrorMessage = "Please Enter Designation")]
        [Display(Name ="Designation")]
        [CustValidation]
        public string designation { get; set; }

    }
}