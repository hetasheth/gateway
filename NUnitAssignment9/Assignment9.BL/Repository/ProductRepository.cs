using System;
using System.Collections.Generic;
using System.Text;
using Assignment9.BL.Model;

namespace Assignment9.BL.Repository
{
    public class ProductRepository
    {
        List<Product> products = new List<Product>();        

        public virtual List<Product> GetProducts()
        {
            return products;
        }

        
    }
}
