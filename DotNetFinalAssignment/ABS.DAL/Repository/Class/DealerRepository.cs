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
    public class DealerRepository : IDealerRepository
    {
        private readonly Database.AppointmentBookingDBEntities _dbContext;

        public DealerRepository()
        {
            _dbContext = new Database.AppointmentBookingDBEntities();
        }

        public Dealer Login(UserLogin user)
        {
            var d = _dbContext.Dealers.Where(x=>x.EmailId==user.EmailID && x.Password==user.Password).FirstOrDefault();
            Dealer dlr = new Dealer();
            if (d!=null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Dealer, Dealer>());
                var mapper = new Mapper(config);
                dlr = mapper.Map<Dealer>(d);
                return dlr;
            }
            return dlr;
        }
    }
}
