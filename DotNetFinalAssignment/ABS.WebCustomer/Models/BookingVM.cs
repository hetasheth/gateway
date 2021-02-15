using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ABS.WebCustomer.Models
{
    public class BookingVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vehicle is require")]
        [Display(Name = "Vehicle")]
        public int? VehicleId { get; set; }

        [Required(ErrorMessage = "Service is require")]
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        [Required(ErrorMessage = "Start time is require")]
        [Display(Name = "Starting time")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End time is require")]
        [Display(Name = "Ending time")]
        public DateTime? EndDateTime { get; set; }
        
        public string Status { get; set; }
    }
}