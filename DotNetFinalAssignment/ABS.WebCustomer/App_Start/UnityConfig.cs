using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using ABS.BAL.Interface;
using ABS.BAL.Class;
using ABS.BAL.HelperClass;

namespace ABS.WebCustomer
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IBookingManager, BookingManager>();
            container.RegisterType<ICustomerManager, CustomerManager>();
            container.RegisterType<IDealerManager, DealerManager>();
            container.RegisterType<IMechanicManager, MechanicManager>();
            container.RegisterType<IServiceManager, ServiceManager>();
            container.RegisterType<IVehicleManager, VehicleManager>();
            container.AddNewExtension<UnityHelper>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}