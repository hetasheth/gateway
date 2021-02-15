using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.Models;

namespace ABS.DAL.Repository.Interface
{
    public interface IDealerRepository
    {
        Dealer Login(UserLogin user);
    }
}
