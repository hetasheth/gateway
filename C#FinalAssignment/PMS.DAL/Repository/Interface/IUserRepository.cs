using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Models;

namespace PMS.DAL.Repository.Interface
{
    public interface IUserRepository
    {
        User Login(UserLogin userLogin);
    }
}
