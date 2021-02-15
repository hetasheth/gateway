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
    public class ServiceRepository : IServiceRepository
    {
        private readonly Database.AppointmentBookingDBEntities _appointmentBookingDBEntities;

        public ServiceRepository()
        {
            _appointmentBookingDBEntities = new Database.AppointmentBookingDBEntities();
        }

        public List<Service> GetServices()
        {
            var list = _appointmentBookingDBEntities.Services.ToList();
            List<Service> lst = new List<Service>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Service, Service>());
                    var mapper = new Mapper(config);
                    Service s = mapper.Map<Service>(item);
                    
                    lst.Add(s);
                }
            }
            return lst;
        }
    }
}
