//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMS.DAL.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
    }
}
