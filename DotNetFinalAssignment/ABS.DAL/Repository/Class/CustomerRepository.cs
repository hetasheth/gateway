using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.DAL.Repository.Interface;
using ABS.Models;
using AutoMapper;

namespace ABS.DAL.Repository.Class
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Database.AppointmentBookingDBEntities _dbContext;

        public CustomerRepository()
        {
            _dbContext = new Database.AppointmentBookingDBEntities();
        }        

        public Customer Login(UserLogin user)
        {
            var c = _dbContext.Customers.Where(x => x.EmailId == user.EmailID && x.Password == user.Password).FirstOrDefault();
            Customer cust = new Customer();
            if (c != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Customer, Customer>());
                var mapper = new Mapper(config);
                cust = mapper.Map<Customer>(c);
                return cust;
            }
            return cust;
        }

        public int Create(Customer customer)
        {
            if (customer != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, Database.Customer>());
                var mapper = new Mapper(config);
                Database.Customer cust = mapper.Map<Database.Customer>(customer);
                _dbContext.Customers.Add(cust);
                _dbContext.SaveChanges();
                return cust.Id;
            }
            else
                return 0;
        }
    }
}
