using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.Models;

namespace ABS.BAL.Interface
{
    public interface IVehicleManager
    {
        List<Vehicle> GetVehiclesByUserId(int userId);
        Vehicle GetVehicleById(int userId);
        int Create(Vehicle vehicle);
        int Update(Vehicle vehicle);
        int Delete(int id);
    }
}
