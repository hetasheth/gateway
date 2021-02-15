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
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(UserLogin userLogin)
        {
            return _userRepository.Login(userLogin);
        }
    }
}
