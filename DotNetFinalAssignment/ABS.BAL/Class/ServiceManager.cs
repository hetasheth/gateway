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
    public class ServiceManager : IServiceManager
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceManager(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public List<Service> GetServices()
        {
            return _serviceRepository.GetServices();
        }
    }
}
