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
    public class VehicleController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(VehicleController));
        private readonly IVehicleManager _vehicleManager;

        public VehicleController(IVehicleManager vehicleManager)
        {
            _vehicleManager = vehicleManager;
        }

        // GET: Vehicle
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "Vehicle List";
            List<Vehicle> lst = new List<Vehicle>();
            try
            {                
                if (Session["uid"] != null)
                {
                    lst = _vehicleManager.GetVehiclesByUserId(Convert.ToInt32(Session["uid"].ToString()));
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
            ViewBag.ActiveMenu = "Vehicle List";
            return View("Create", new VehicleVM());
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "Vehicle List";
            try
            {
                Vehicle vehicle = _vehicleManager.GetVehicleById(id);
                VehicleVM vvm = new VehicleVM();
                if(vehicle!=null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle, VehicleVM>());
                    var mapper = new Mapper(config);
                    vvm = mapper.Map<VehicleVM>(vehicle);
                }
                return View("Create", vvm);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(VehicleVM vvm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<VehicleVM, Vehicle>());
                    var mapper = new Mapper(config);
                    Vehicle vehicle = mapper.Map<Vehicle>(vvm);
                    vehicle.CustomerId = Convert.ToInt32(Session["uid"]);
                    int vid;
                    if (vvm.Id == 0)
                        vid = _vehicleManager.Create(vehicle);
                    else
                        vid = _vehicleManager.Update(vehicle);
                    if (vid != 0)
                    {
                        return RedirectToAction("Index", "Vehicle");
                    }
                    else
                    {
                        TempData["invalidLogin"] = "Something went wrong!!!";
                        return RedirectToAction("Create");
                    }

                }
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Create");
            }
        }

        public ActionResult Delete(int id)
        {
            ViewBag.ActiveMenu = "Vehicle List";
            try
            {
                int vid = _vehicleManager.Delete(id);

                if (vid != 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["invalidLogin"] = "Something went wrong!!!";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Index");
            }
        }
    }
}