using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.DAL.Repository.Class;
using PMS.DAL.Repository.Interface;
using Unity;
using Unity.Extension;

namespace PMS.BAL.Helper
{
    public class UnityHelper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IUserRepository, UserRepository>();
            Container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}
