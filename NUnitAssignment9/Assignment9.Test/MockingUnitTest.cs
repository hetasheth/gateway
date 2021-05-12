using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Assignment9.BL.Model;
using Assignment9.BL.Repository;
using NUnit.Framework;
using Moq;

namespace Assignment9.Test
{
    public class MockingUnitTest
    {
        private ProductRepository productRepository;

        [SetUp]
        public void SetUp()
        {
            var productMockRepository = new Mock<ProductRepository>();
            productMockRepository.Setup(x => x.GetProducts()).Returns(
                new List<Product>() {
                    new Product
                    {
                        ProductId=1,
                        ProductName="Mobile",
                        Color="White",
                        Price=20000
                    },
                    new Product
                    {
                        ProductId=2,
                        ProductName="Laptop",
                        Color="Black",
                        Price=30000
                    },
                    new Product
                    {
                        ProductId=3,
                        ProductName="Tablet",
                        Color="Grey",
                        Price=25000
                    }
                });
            productRepository = productMockRepository.Object;
        }

        [Test]
        public void ProductListCountTestPositive()
        {
            //Assert 
            Assert.True(productRepository.GetProducts().Count == 3);
        }

        [Test]
        public void ProductListCountTestNegative()
        {
            //Assert 
            Assert.False(productRepository.GetProducts().Count == 5);
        }

        [Test]
        public void GetProductByIdTest()
        {
            // Act
            var result = productRepository.GetProducts().FirstOrDefault(x => x.ProductId == 1);

            //Assert 
            Assert.AreEqual("Mobile", result.ProductName);
        }

        [Test]
        public void GetProductByColorTest()
        {
            // Act
            var result = productRepository.GetProducts().FirstOrDefault(x => x.Color == "Grey");

            //Assert 
            Assert.AreEqual("Tablet", result.ProductName);
        }

        [Test]
        public void GetProductByPriceTest()
        {
            // Act
            var result = productRepository.GetProducts().FirstOrDefault(x => x.Price >= 26000);

            //Assert 
            Assert.AreEqual("Laptop", result.ProductName);
        }
    }
}
