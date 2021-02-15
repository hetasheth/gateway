using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.Models;

namespace ABS.BAL.Interface
{
    public interface IServiceManager
    {
        List<Service> GetServices();
    }
}
