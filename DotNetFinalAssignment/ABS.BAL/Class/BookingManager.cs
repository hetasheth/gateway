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
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public List<Booking> GetBookings()
        {
            return _bookingRepository.GetBookings();
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            return _bookingRepository.GetBookingsByUserId(userId);
        }

        public Booking GetBookingById(int id)
        {
            return _bookingRepository.GetBookingById(id);
        }

        public int Create(Booking booking)
        {
            return _bookingRepository.Create(booking);
        }

        public int Update(Booking booking)
        {
            return _bookingRepository.Update(booking);
        }
    }
}
