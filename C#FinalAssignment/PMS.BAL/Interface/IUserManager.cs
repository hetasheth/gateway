using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Models;

namespace PMS.BAL.Interface
{
    public interface IUserManager
    {
        User Login(UserLogin userLogin);
    }
}
