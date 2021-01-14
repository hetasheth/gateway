using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Models;

namespace HMS.DAL.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Database.HotelManageSystemDBEntities _dbContext;

        public BookingRepository()
        {
            _dbContext = new Database.HotelManageSystemDBEntities();
        }

        enum BookingsStatus
        {
            Optional,
            Definitive,
            Cancelled,
            Deleted
        }

        public string Create(Booking model)
        {
            try
            {
                Database.Booking booking = new Database.Booking();

                if (model != null)
                {
                    booking.RoomID = model.RoomID;
                    booking.BookingDate = model.BookingDate;
                    booking.Status = BookingsStatus.Optional.ToString();
                    _dbContext.Bookings.Add(booking);
                    _dbContext.SaveChanges();
                    return "Success";
                }
                return "Model is null";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateBookingDate(Booking model)
        {
            try
            {
                var booking = _dbContext.Bookings.Where(x => x.RoomID==model.RoomID).FirstOrDefault();
                if(booking!=null)
                {
                    booking.RoomID = model.RoomID;
                    booking.BookingDate = model.BookingDate;
                    _dbContext.SaveChanges();
                    return "Success";
                }
                return "No data found!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateStatus(Booking model)
        {
            try
            {
                var booking = _dbContext.Bookings.Find(model.ID);
                if (booking != null)
                {
                    booking.ID = model.ID;
                    booking.Status = model.Status;
                    _dbContext.SaveChanges();
                    return "Success";
                }
                return "No data found!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(long id)
        {
            try
            {
                var booking = _dbContext.Bookings.Find(id);
                if (booking != null)
                {
                    booking.Status = "Deleted";
                    _dbContext.SaveChanges();
                    return "Success";
                }
                return "No data found!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
