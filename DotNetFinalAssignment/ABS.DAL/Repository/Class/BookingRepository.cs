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
    public class BookingRepository : IBookingRepository
    {
        private readonly Database.AppointmentBookingDBEntities _dbContext;

        public BookingRepository()
        {
            _dbContext = new Database.AppointmentBookingDBEntities();
        }

        public List<Booking> GetBookings()
        {
            var lst = _dbContext.Bookings.ToList();
            List<Booking> bookings = new List<Booking>();
            if (lst != null)
            {
                foreach (var items in lst)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Booking, Booking>());
                    var mapper = new Mapper(config);
                    Booking booking = mapper.Map<Booking>(items);
                    bookings.Add(booking);
                }
                return bookings;
            }
            return bookings;
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            var lst = _dbContext.Bookings.Where(x => x.Vehicle.CustomerId == userId).ToList();
            List<Booking> bookings = new List<Booking>();
            if (lst != null)
            {
                foreach (var items in lst)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Booking, Booking>());
                    var mapper = new Mapper(config);
                    Booking booking = mapper.Map<Booking>(items);
                    bookings.Add(booking);
                }
                return bookings;
            }
            return bookings;
        }

        public Booking GetBookingById(int id)
        {
            var lst = _dbContext.Bookings.Find(id);
            Booking booking = new Booking();
            if (lst != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Booking, Booking>());
                var mapper = new Mapper(config);
                booking = mapper.Map<Booking>(lst);
                return booking;
            }
            return booking;
        }

        public int Create(Booking booking)
        {
            if (booking != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, Database.Booking>());
                var mapper = new Mapper(config);
                Database.Booking b = mapper.Map<Database.Booking>(booking);
                _dbContext.Bookings.Add(b);
                _dbContext.SaveChanges();
                return b.Id;
            }
            else
                return 0;
        }

        public int Update(Booking booking)
        {
            if (booking != null)
            {
                var obj = _dbContext.Bookings.Find(booking.Id);
                if (obj != null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, Database.Booking>());
                    var mapper = new Mapper(config);
                    mapper.Map<Booking, Database.Booking>(booking, obj);
                    _dbContext.SaveChanges();
                    return obj.Id;
                }
                else
                    return 0;
            }
            else
                return 0;
        }
    }
}
