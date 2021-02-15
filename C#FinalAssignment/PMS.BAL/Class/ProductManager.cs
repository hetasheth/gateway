using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.BAL.Interface;
using PMS.DAL.Repository.Interface;
using PMS.Models;

namespace PMS.BAL.Class
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public string Create(Product product)
        {
            return _productRepository.Create(product);
        }

        public string Update(int id,Product product)
        {
            return _productRepository.Update(id,product);
        }

        public string Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public string DeleteMultiple(string[] ids)
        {
            return _productRepository.DeleteMultiple(ids);
        }
    }
}
