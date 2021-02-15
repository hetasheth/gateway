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
    public class DealerManager : IDealerManager
    {
        private readonly IDealerRepository _dealerRepository;

        public DealerManager(IDealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }

        public Dealer Login(UserLogin user)
        {
            return _dealerRepository.Login(user);
        }
    }
}
