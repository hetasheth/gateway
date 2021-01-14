using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public interface IBookingRepository
    {
        string Create(Booking model);
        string UpdateBookingDate(Booking model);
        string UpdateStatus(Booking model);
        string Delete(long id);
    }
}
