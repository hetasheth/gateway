using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABS.WebDealer.Models;
using ABS.Models;
using ABS.BAL.Interface;
using AutoMapper;

namespace ABS.WebDealer.Controllers
{
    public class DealerController : Controller
    {
        private readonly IDealerManager _dealerManager;

        public DealerController(IDealerManager dealerManager)
        {
            _dealerManager = dealerManager;
        }

        // GET: Dealer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(LoginVM lvm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<LoginVM, UserLogin>());
                    var mapper = new Mapper(config);
                    UserLogin usr = mapper.Map<UserLogin>(lvm);
                    Dealer dealer = _dealerManager.Login(usr);
                    if (dealer.Name != null)
                    {
                        Session["uname"] = dealer.Name;
                        return RedirectToAction("Index", "Booking");
                    }

                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                //log.Error("Exception : " + ex);
                return View("Login");
            }
        }
    }
}