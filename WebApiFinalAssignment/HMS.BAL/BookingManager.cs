using HMS.BAL.Interface;
using HMS.DAL.Repository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public string Create(Booking model)
        {
            return _bookingRepository.Create(model);
        }        

        public string UpdateBookingDate(Booking model)
        {
            return _bookingRepository.UpdateBookingDate(model);
        }

        public string UpdateStatus(Booking model)
        {
            return _bookingRepository.UpdateStatus(model);
        }

        public string Delete(long id)
        {
            return _bookingRepository.Delete(id);
        }
    }
}
