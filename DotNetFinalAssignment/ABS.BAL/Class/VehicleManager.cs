using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.BAL.Interface;
using ABS.DAL.Repository.Interface;
using ABS.Models;

namespace ABS.BAL.Class
{
    public class VehicleManager : IVehicleManager
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleManager(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public List<Vehicle> GetVehiclesByUserId(int userId)
        {
            return _vehicleRepository.GetVehiclesByUserId(userId);
        }

        public Vehicle GetVehicleById(int id)
        {
            return _vehicleRepository.GetVehicleById(id);
        }

        public int Create(Vehicle vehicle)
        {
            return _vehicleRepository.Create(vehicle);
        }

        public int Update(Vehicle vehicle)
        {
            return _vehicleRepository.Update(vehicle);
        }

        public int Delete(int id)
        {
            return _vehicleRepository.Delete(id);
        }
    }
}
