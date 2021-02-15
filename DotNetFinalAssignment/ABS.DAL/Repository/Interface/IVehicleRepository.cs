using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.Models;

namespace ABS.DAL.Repository.Interface
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetVehiclesByUserId(int userId);
        Vehicle GetVehicleById(int id);
        int Create(Vehicle vehicle);
        int Update(Vehicle vehicle);
        int Delete(int id);
    }
}
