using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABS.BAL.Interface;
using ABS.Models;
using ABS.WebDealer.Models;
using log4net;
using AutoMapper;

namespace ABS.WebDealer.Controllers
{
    public class BookingController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(BookingController));
        private readonly IBookingManager _bookingManager;

        public BookingController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;

        }
        // GET: Booking
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "Bookings";
            List<Booking> lst = new List<Booking>();
            try
            {
                lst = _bookingManager.GetBookings();

                return View(lst);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View(lst);
            }
        }
    }
}