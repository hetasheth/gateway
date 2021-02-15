using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABS.Models;
using ABS.WebCustomer.Models;
using ABS.BAL.Interface;
using AutoMapper;
using log4net;

namespace ABS.WebCustomer.Controllers
{
    public class CustomerController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(CustomerController));
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        // GET: Customer
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
                    Customer customer= _customerManager.Login(usr);
                    if(customer.Name!=null)
                    {
                        Session["uid"] = customer.Id;
                        Session["uname"] = customer.Name;
                        return RedirectToAction("Index", "Vehicle");
                    }
                    else
                    {
                        TempData["invalidLogin"] = "Invalid Username or Password!!!";
                        return RedirectToAction("Login");
                    }

                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Login");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterVM rvm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterVM, Customer>());
                    var mapper = new Mapper(config);
                    Customer cust = mapper.Map<Customer>(rvm);
                    int custId = _customerManager.Create(cust);
                    if (custId!=0)
                    {
                        return RedirectToAction("Login", "Customer");
                    }
                    else
                    {
                        TempData["invalidLogin"] = "Something went wrong!!!";
                        return RedirectToAction("Register");
                    }

                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Register");
            }
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            Session["uid"] = null;
            Session["uname"] = null;
            return RedirectToAction("Login");
        }
    }
}