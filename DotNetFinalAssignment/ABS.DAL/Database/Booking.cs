//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ABS.DAL.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public int Id { get; set; }
        public Nullable<int> VehicleId { get; set; }
        public Nullable<int> MechanicId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public Nullable<int> BookedBy { get; set; }
        public string Status { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Mechanic Mechanic { get; set; }
        public virtual Service Service { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
