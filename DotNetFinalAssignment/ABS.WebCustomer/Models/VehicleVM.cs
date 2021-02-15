using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ABS.WebCustomer.Models
{
    public class VehicleVM
    {
        public int Id { get; set; }

        [Display(Name = "License Plate")]
        [Required(ErrorMessage = "License Plate is Required")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "License Plate should be between 8-10 characters")]
        public string LicencePlate { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Brand is Required")]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Brand should be between 4-25 characters")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Model is Required")]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Model should be between 4-25 characters")]
        public string Model { get; set; }

        [Display(Name = "Registration Date")]
        [Required(ErrorMessage = "Registration Date is Required")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Chassis No")]
        [Required(ErrorMessage = "Chassis No is Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Chassis No should be between 5-50 characters")]
        public string ChassisNo { get; set; }

        [Display(Name = "Owner Name")]
        [Required(ErrorMessage = "Owner Name is Required")]
        public string OwnerName { get; set; }
    }
}