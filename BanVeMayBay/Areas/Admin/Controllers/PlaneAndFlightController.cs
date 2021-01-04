using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeMayBay.Models;
using BanVeMayBay.Areas.Admin.Data;
using System.Data.Entity;
using PagedList;
namespace BanVeMayBay.Areas.Admin.Controllers
{
    public class PlaneAndFlightController : Controller
    {
        CNPM_DHT_NHOM19Entities db = new CNPM_DHT_NHOM19Entities();
        // GET: Admin/PlaneAndFlight
        public ActionResult Index()
        {
            ViewBag.planeList = PlaneList().ToPagedList(1,5);
            ViewBag.TypePlane = TypePlaneList();
            ViewBag.airportList = AirportList().ToPagedList(1, 5);
            return View();
        }
        public ActionResult AddAirport()
        {
            AirportManager ap = new AirportManager();
            return View(ap);
        }
        [HttpPost]
        public ActionResult AddAirport(AirportManager ap)
        {
            if (ModelState.IsValid)
            {
                Airport app = new Airport();
                app.Name = ap.Name;
                app.Name = ap.Nation;
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
            ViewBag.typePlane = new SelectList(TypePlaneSelectList(), "value", "text");
            Plane p = new Plane();
            return View("AddPlane", p);
        }
        [HttpPost]
        public ActionResult AddPlane(Plane p)
        {
            if(ModelState.IsValid)
            {
                p.TongSoGhe = p.SoGheHang1 + p.SoGheHang2;
                p.CreateDate = DateTime.Now;
                p.ModifyDate = DateTime.Now;
                db.Entry(p).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.typePlane = new SelectList(TypePlaneSelectList(), "value", "text");
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

        public List<Airport> AirportList()
        {
            var apl = db.Airports.ToList();
            return apl;
        }
        public List<SelectListItem> AirportListItem()
        {
            List<SelectListItem> apsl = new List<SelectListItem>();
            var apl = AirportList();
            foreach(var item in apl)
            {
                SelectListItem ap = new SelectListItem();
                ap.Value = item.Id.ToString();
                ap.Text = item.Name;
                apsl.Add(ap);
            }
            return apsl;
        }

        public List<TypePlane> TypePlaneList()
        {
            return db.TypePlanes.ToList();
        }
        public List<SelectListItem> TypePlaneSelectList()
        {
            List<SelectListItem> tpsl = new List<SelectListItem>();
            var tpl = TypePlaneList();
            foreach (var item in tpl)
            {
                SelectListItem ap = new SelectListItem();
                ap.Value = item.Id.ToString();
                ap.Text = item.Name;
                tpsl.Add(ap);
            }
            return tpsl;
        }

        public List<Plane> PlaneList()
        {
            return db.Planes.ToList();
        }

    }
}