using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalApp.CustValids
{
    public class AgeValid:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = (DateTime)value;
            if (dt <= DateTime.Now.AddYears(-18))
                return true;
            else
                return false;
        }
    }
}