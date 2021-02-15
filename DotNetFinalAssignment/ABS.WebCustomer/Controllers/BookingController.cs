using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABS.BAL.Interface;
using ABS.Models;
using ABS.WebCustomer.Models;
using log4net;
using AutoMapper;

namespace ABS.WebCustomer.Controllers
{
    public class BookingController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(BookingController));
        private readonly IBookingManager _bookingManager;
        private readonly IVehicleManager _vehicleManager;
        private readonly IServiceManager _serviceManager;

        public BookingController(IBookingManager bookingManager,IVehicleManager vehicleManager,IServiceManager serviceManager)
        {
            _bookingManager = bookingManager;
            _vehicleManager = vehicleManager;
            _serviceManager = serviceManager;
        }

        // GET: Booking
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "Bookings";
            List<Booking> lst = new List<Booking>();
            try
            {
                if (Session["uid"] != null)
                {
                    lst = _bookingManager.GetBookingsByUserId(Convert.ToInt32(Session["uid"].ToString()));
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View(lst);
            }
        }

        public ActionResult Create()
        {
            var vlst = new SelectList(_vehicleManager.GetVehiclesByUserId(Convert.ToInt32(Session["uid"])), "Id", "LicencePlate");
            ViewData["vehicleList"] = vlst;
            var lst = new SelectList(_serviceManager.GetServices(),"Id","ServiceName");
            ViewData["serviceList"] = lst;            
            ViewBag.ActiveMenu = "Bookings";
            return View("Create", new BookingVM());
        }

        public ActionResult Edit(int id)
        {
            var vlst = new SelectList(_vehicleManager.GetVehiclesByUserId(Convert.ToInt32(Session["uid"])), "Id", "LicencePlate");
            ViewData["vehicleList"] = vlst;
            var lst = new SelectList(_serviceManager.GetServices(), "Id", "ServiceName");
            ViewData["serviceList"] = lst;
            ViewBag.ActiveMenu = "Bookings";
            try
            {
                Booking booking = _bookingManager.GetBookingById(id);
                BookingVM bvm = new BookingVM();
                if (booking != null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingVM>());
                    var mapper = new Mapper(config);
                    bvm = mapper.Map<BookingVM>(booking);
                }
                return View("Create", bvm);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(BookingVM bvm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<BookingVM, Booking>());
                    var mapper = new Mapper(config);
                    Booking booking = mapper.Map<Booking>(bvm);                    
                    booking.BookedBy= Convert.ToInt32(Session["uid"]);
                    booking.MechanicId = null;
                    booking.Status = "Pending";
                    int bid;
                    if (bvm.Id == 0)
                        bid = _bookingManager.Create(booking);
                    else
                        bid = _bookingManager.Update(booking);
                    if (bid != 0)
                    {
                        return RedirectToAction("Index", "Booking");
                    }
                    else
                    {
                        TempData["invalidLogin"] = "Something went wrong!!!";
                        return RedirectToAction("Create");
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Index");
            }
        }
    }
}