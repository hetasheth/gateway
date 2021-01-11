using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PMSApplication;
using PMSApplication.Controllers;
using PMSApplication.Models;

namespace PMSApplication.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        ProductController controller = new ProductController();

        [TestMethod]
        public void Index()
        {
            controller.Index("Productname", "desc", 1, 5, "", false);
        }

        [TestMethod]
        public void IndexPost()
        {
            FormCollection collection = new FormCollection();
            string str = "28,29";
            collection.Add("ID", str);
            controller.Index(collection);
        }

        [TestMethod]
        public void Create()
        {
            controller.Create();
        }

        [TestMethod]
        public void Edit()
        {
            controller.Edit(28);
        }

        
        [TestMethod]
        public void CreateOrEdit()
        {
            string filePath = Path.GetFullPath(@"C:\Users\acer\Desktop\gateway\PMSWebApi\PMSApplication\Images\original.jpg");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            ProductModel pmodel = new ProductModel
            {
                ProductID = 0,
                ProductName = "xyz",
                Category = "xyz",
                Price = 110,
                Quantity = 5,
                ShortDescription = "xyz",
                LongDescription = "xyz",
                SmallImage =uploadedFile.Object,
                LargeImage=uploadedFile.Object
            };
            controller.CreateOrEdit(pmodel);
        }

        [TestMethod]
        public void Delete()
        {
            controller.Delete(28);
        }
    }
}
