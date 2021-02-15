using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.DAL.Repository.Interface;
using PMS.Models;
using AutoMapper;

namespace PMS.DAL.Repository.Class
{
    public class ProductRepository : IProductRepository
    {
        private readonly Database.PMSystemDBEntities _dbContext;

        public ProductRepository()
        {
            _dbContext = new Database.PMSystemDBEntities();
        }

        public List<Product> GetProducts()
        {
            var pd = _dbContext.Products.ToList();
            List<Product> lst = new List<Product>();
            if (pd != null)
            {
                foreach (var items in pd)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Product, Product>());
                    var mapper = new Mapper(config);
                    Product prod = mapper.Map<Product>(items);
                    lst.Add(prod);
                }
                return lst;
            }
            return lst;
        }

        public Product GetProductById(int id)
        {
            var pd = _dbContext.Products.Find(id);
            Product prod = new Product();
            if (pd != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Product, Product>());
                var mapper = new Mapper(config);
                prod = mapper.Map<Product>(pd);
                return prod;
            }
            return prod;
        }

        public string Create(Product product)
        {
            if (product != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, Database.Product>());
                var mapper = new Mapper(config);
                Database.Product prod = mapper.Map<Database.Product>(product);
                _dbContext.Products.Add(prod);
                _dbContext.SaveChanges();
                return "Success";
            }
            else
                return "Null Data";
        }

        public string Update(int id,Product product)
        {
            if (product != null)
            {
                var pd = _dbContext.Products.Find(id);
                if (pd != null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, Database.Product>());
                    var mapper = new Mapper(config);
                    mapper.Map(product,pd);
                    _dbContext.SaveChanges();
                    return "Success";
                }
                else
                    return "No Data Found!!";
            }
            else
                return "Null Data";
        }

        public string Delete(int id)
        {
            var pd = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(pd);
            _dbContext.SaveChanges();
            return "Success";
        }

        public string DeleteMultiple(string[] ids)
        {
            if (ids.Count() > 0)
            {
                foreach (string pid in ids)
                {
                    var pd = _dbContext.Products.Find(Convert.ToInt32(pid));
                    _dbContext.Products.Remove(pd);
                    _dbContext.SaveChanges();
                }
                return "Success";
            }
            else
                return "No Data Found!!!";
            
        }
    }
}
