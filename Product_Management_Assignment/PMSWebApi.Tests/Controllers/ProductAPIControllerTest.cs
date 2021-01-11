using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMSWebApi;
using PMSWebApi.Controllers;
using PMSWebApi.Models;

namespace PMSWebApi.Tests.Controllers
{
    [TestClass]
    public class ProductAPIControllerTest
    {
        ProductAPIController controller = new ProductAPIController();

        [TestMethod]
        public void Get()
        {            
            controller.Get(27);
        }

        [TestMethod]
        public void GetAdvanceList()
        {
            controller.GetAdvanceList("ProductName", "asc", 2, 2, "", false);
        }

        [TestMethod]
        public void Post()
        {
            Product prod = new Product {
                ProductID=0,
                ProductName="xyz",
                Category="xyz",
                Price=110,
                Quantity=5,
                ShortDescription="xyz",
                LongDescription="xyz",
                SmallImage="xyz",
                LargeImage="xyz"
            };
            controller.Post(prod);
        }

        [TestMethod]
        public void Put()
        {
            Product prod = new Product
            {
                ProductID = 28,
                ProductName = "abc",
                Category = "abc",
                Price = 110,
                Quantity = 5,
                ShortDescription = "xyz",
                LongDescription = "xyz",
                SmallImage = "xyz",
                LargeImage = "xyz"
            };
            controller.Put(28,prod);
        }

        [TestMethod]
        public void Delete()
        {
            controller.Delete(29);
        }

        [TestMethod]
        public void DeleteMultiple()
        {
            string[] ids = { "30", "31" };
            controller.DeleteMultiple(ids);
        }
    }
}
