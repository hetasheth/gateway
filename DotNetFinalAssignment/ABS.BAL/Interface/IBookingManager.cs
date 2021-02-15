using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABS.Models;

namespace ABS.BAL.Interface
{
    public interface IBookingManager
    {
        List<Booking> GetBookings();
        List<Booking> GetBookingsByUserId(int userId);
        Booking GetBookingById(int id);
        int Create(Booking booking);
        int Update(Booking booking);
    }
}
