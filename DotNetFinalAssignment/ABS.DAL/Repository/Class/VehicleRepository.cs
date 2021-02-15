using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.DAL.Repository.Interface;
using ABS.Models;
using AutoMapper;

namespace ABS.DAL.Repository.Class
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly Database.AppointmentBookingDBEntities _dbContext;

        public VehicleRepository()
        {
            _dbContext = new Database.AppointmentBookingDBEntities();
        }

        public List<Vehicle> GetVehiclesByUserId(int userId)
        {
            var lst = _dbContext.Vehicles.Where(x => x.CustomerId == userId).ToList();
            List<Vehicle> vehicles = new List<Vehicle>();
            if (lst != null)
            {
                foreach (var items in lst)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Vehicle, Vehicle>());
                    var mapper = new Mapper(config);
                    Vehicle vehicle = mapper.Map<Vehicle>(items);
                    vehicles.Add(vehicle);
                }
                return vehicles;
            }
            return vehicles;
        }

        public Vehicle GetVehicleById(int id)
        {
            var lst = _dbContext.Vehicles.Find(id);
            Vehicle vehicle = new Vehicle();
            if (lst != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Vehicle, Vehicle>());
                var mapper = new Mapper(config);
                vehicle = mapper.Map<Vehicle>(lst);
                return vehicle;
            }
            return vehicle;
        }

        public int Create(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle, Database.Vehicle>());
                var mapper = new Mapper(config);
                Database.Vehicle v = mapper.Map<Database.Vehicle>(vehicle);
                _dbContext.Vehicles.Add(v);
                _dbContext.SaveChanges();
                return v.Id;
            }
            else
                return 0;
        }

        public int Update(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                var obj = _dbContext.Vehicles.Find(vehicle.Id);
                if (obj != null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle, Database.Vehicle>());
                    var mapper = new Mapper(config);
                    mapper.Map<Vehicle, Database.Vehicle>(vehicle, obj);
                    _dbContext.SaveChanges();
                    return obj.Id;
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        public int Delete(int id)
        {
            var obj = _dbContext.Vehicles.Find(id);
            _dbContext.Vehicles.Remove(obj);
            _dbContext.SaveChanges();
            return 1;
        }
    }
}
