using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Models;

namespace PMS.BAL.Interface
{
    public interface IProductManager
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        string Create(Product product);
        string Update(int id,Product product);
        string Delete(int id);
        string DeleteMultiple(string[] ids);
    }
}
