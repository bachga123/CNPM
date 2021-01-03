using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeMayBay.Models;
using BanVeMayBay.Areas.Admin.Data;
using System.Data.Entity;
namespace BanVeMayBay.Areas.Admin.Controllers
{
    public class PlaneAndFlightController : Controller
    {
        CNPM_DHT_NHOM19Entities db = new CNPM_DHT_NHOM19Entities();
        // GET: Admin/PlaneAndFlight
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddAirport()
        {
            AirportManager ap = new AirportManager();
            return View(ap);
        }
        public ActionResult AddAirport(AirportManager ap)
        {
            if (ModelState.IsValid)
            {
                Airport app = new Airport();
                app.Name = ap.Name;
                app.Name = ap.Nation;
                app.CreateDate = DateTime.Now;
                app.ModifyDate = DateTime.Now;
                db.Entry(app).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddAirport", ap);
            }
        }
        public ActionResult EditAirport(int apid)
        {
            var ap = db.Airports.Find(apid);
            AirportManager app = new AirportManager();
            app.Name = ap.Name;
            app.Nation = ap.Nation;
            app.id = ap.Id;
            return View(app);
        }
        [HttpPost]
        public ActionResult EditAirport(AirportManager ap)
        {
            if(ModelState.IsValid)
            {
                var app = db.Airports.Find(ap.id);
                app.Name = ap.Name;
                app.Nation = ap.Nation;
                app.ModifyDate = DateTime.Now;
                db.Entry(app).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");     
            }
            else
            {
                return View("EditAirport", ap);
            }
        }
        public ActionResult DeleteAirport(int apid)
        {
            var ap = db.Airports.Find(apid);
            db.Entry(ap).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddTypePlane()
        {
            TypePlane tp = new TypePlane();
            return View(tp);
        }
        [HttpPost]
        public ActionResult AddTypePlane(TypePlane tp)
        {
            if(ModelState.IsValid)
            {
                db.Entry(tp).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", tp);
            }
        }

        public ActionResult EditTypePlane(int typeid)
        {
            var tp = db.TypePlanes.Find(typeid);
            return View("EditTypePlane", tp);
        }
        [HttpPost]
        public ActionResult EditTypePlane(TypePlane tp)
        {
            if(ModelState.IsValid)
            {
                db.Entry(tp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditTypePlane", tp);
            }
        }
        public ActionResult DeleteTypePlane(int tpid)
        {
            var tp = db.TypePlanes.Find(tpid);
            db.Entry(tp).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddPlane()
        {
            Plane p = new Plane();
            return View("AddPlane", p);
        }
        [HttpPost]
        public ActionResult AddPlane(Plane p)
        {
            if(ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddPlane", p);
            }
        }

        public ActionResult EditPlane(int pid)
        {
            var p = db.Planes.Find(pid);
            return View("EditPlane", p);
        }
        [HttpPost]
        public ActionResult EditPlane(Plane p)
        {
            if(ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditPlane", p);
            }
        }
        public ActionResult DeletePlane(int pid)
        {
            var p = db.Planes.Find(pid);
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}