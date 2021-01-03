using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeMayBay.Models;
namespace BanVeMayBay.Areas.Employee.Controllers
{
    public class TicketController : Controller
    {
        CNPM_DHT_NHOM19Entities db = new CNPM_DHT_NHOM19Entities();
        // GET: Employee/Ticket
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddFlight()
        {
            Flight f = new Flight();
            return View("AddFlight", f);
        }
        [HttpPost]
        public ActionResult AddFlight(Flight f)
        {
            if(ModelState.IsValid)
            {
                db.Entry(f).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddFlight", f);
            }
        }
        public ActionResult EditFlight(int fid)
        {
            var f = db.Flights.Find(fid);
            return View("EditFlight", f);
        }
        [HttpPost]
        public ActionResult EditFlight(Flight f)
        {
            if(ModelState.IsValid)
            {
                db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditFlight", f);
            }
        }
        public ActionResult DeleteFlight(int fid)
        {
            var f = db.Flights.Find(fid);
            db.Entry(f).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddTicket()
        {
            Ticket t = new Ticket();
            return View("AddTicket", t);
        }
        [HttpPost]
        public ActionResult AddTicket(Ticket t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddTicket", t);
            }

        }
        public ActionResult EditTicket(int tid)
        {
            Ticket t = db.Tickets.Find(tid);
            return View("AddTicket", t);
        }
        [HttpPost]
        public ActionResult EditTicket(Ticket t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditTicket", t);
            }
        }
        public ActionResult DeleteTicket(int tid)
        {
            var t = db.Tickets.Find(tid);
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddTicketType()
        {
            TypeTicket t = new TypeTicket();
            return View("AddTicket", t);
        }

        public ActionResult AddTicketType(TypeTicket t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddTicket", t);
            }

        }
        public ActionResult EditTypeTicket(int tid)
        {
            TypeTicket t = db.TypeTickets.Find(tid);
            return View("AddTicket", t);
        }
        [HttpPost]
        public ActionResult EditTypeTicket(TypeTicket t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditTicket", t);
            }
        }
        public ActionResult DeleteTypeTicket(int tid)
        {
            var t = db.TypeTickets.Find(tid);
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}