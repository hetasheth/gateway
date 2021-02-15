using System.Web.Http;
using PMS.BAL.Interface;
using PMS.BAL.Class;
using PMS.BAL.Helper;
using Unity;
using Unity.WebApi;

namespace PMS.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserManager, UserManager>();
            container.RegisterType<IProductManager, ProductManager>();
            container.AddNewExtension<UnityHelper>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}