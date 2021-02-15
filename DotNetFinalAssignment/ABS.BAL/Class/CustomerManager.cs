using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.BAL.Interface;
using ABS.DAL.Repository.Interface;
using ABS.Models;

namespace ABS.BAL.Class
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }        

        public Customer Login(UserLogin user)
        {
            return _customerRepository.Login(user);
        }

        public int Create(Customer customer)
        {
            return _customerRepository.Create(customer);
        }
    }
}
