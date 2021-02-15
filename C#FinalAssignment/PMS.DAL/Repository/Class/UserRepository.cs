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
    public class UserRepository : IUserRepository
    {
        private readonly Database.PMSystemDBEntities _dbContext;

        public UserRepository()
        {
            _dbContext = new Database.PMSystemDBEntities();
        }

        public User Login(UserLogin userLogin)
        {
            var obj = _dbContext.Users.Where(x => x.EmailID == userLogin.EmailID && x.Password == userLogin.Password).FirstOrDefault();
            User usr = new User();
            if (obj != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.User, User>());
                var mapper = new Mapper(config);
                usr = mapper.Map<User>(obj);
                return usr;
            }
            return usr;
        }
    }
}
