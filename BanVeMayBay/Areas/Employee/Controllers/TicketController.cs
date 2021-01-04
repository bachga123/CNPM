using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeMayBay.Models;
using PagedList;
namespace BanVeMayBay.Areas.Employee.Controllers
{
    public class TicketController : Controller
    {
        CNPM_DHT_NHOM19Entities db = new CNPM_DHT_NHOM19Entities();
        // GET: Employee/Ticket
        public ActionResult Index()
        {
            ViewBag.ticketTemp = GetTicketTempList();
            return View();
        }
        public ActionResult FlightList(int page = 1, int pageSize = 10)
        {
            var flightList = GetFlightList().OrderBy(m => m.NgayGio).ToPagedList(page,pageSize);
            return View(flightList) ;
        }
        public ActionResult FlightDetails(int id)
        {
            ViewBag.airStop = AirStopList(id).ToPagedList(1,2);
            var flight = db.Flights.Find(id);
            return View("FlightDetails",flight);
        }
        public ActionResult AddFlight()
        {
            Session["planeList"] = new SelectList(PlaneSelectList(), "value", "text");
            Session["apList"] = new SelectList(AirportSelectListItem(), "value", "text");
            Flight f = new Flight();
            return View("AddFlight", f);
        }
        [HttpPost]
        public ActionResult AddFlight(Flight f)
        {
            if (ModelState.IsValid)
            {
                if(f.NgayGio == null || f.SoLuongGheHang1 == null || f.SoLuongGheHang2 == null || f.ThoiGianbay == null)
                {
                    ModelState.AddModelError("", "Điền tất cả các ô còn trống");
                    return View("AddFlight", f);
                }
                if(f.ThoiGianbay < 30)
                {
                    
                    ModelState.AddModelError("", "Thời gian bay phải lớn hơn 30 phút.");
                    return View("AddFlight", f);
                }
                db.Entry(f).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("FlightList");
            }
            else
            {            
                return View("AddFlight", f);
            }
        }
        public ActionResult AddStopStationForFlight(int id)
        {
            Session["apList"] = new SelectList(AirportSelectListItem(), "value", "text");
            AirStopOver aso = new AirStopOver();
            aso.FlightId = id;
            return View("AddStopStationForFlight",aso);
        }
        [HttpPost]
        public ActionResult AddStopStationForFlight(AirStopOver aso)
        {
            if (ModelState.IsValid)
            {
                if(aso.ThoiGianDung == null)
                {
                    ModelState.AddModelError("", "Chọn thời gian dừng");
                    return View("AddStopStationForFlight", aso);
                }
                if(aso.ThoiGianDung > 20 || aso.ThoiGianDung < 10)
                {
                    ModelState.AddModelError("", "Thời gian dừng phải từ khoảng 10 -20 phút");
                    return View("AddStopStationForFlight", aso);
                }
                else if(!CheckIfMoreThan2AirStop((int)aso.FlightId))
                {
                    ModelState.AddModelError("", "chuyến bay này đã có tối đa 2 trạm dừng chân");
                    return View("AddStopStationForFlight", aso);
                }
                else
                {
                    db.Entry(aso).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("FlightDetails",new { id = aso.FlightId});
                }
            }
            else
            {
                return View("AddStopStationForFlight", aso);
            }
        }
        public bool CheckIfMoreThan2AirStop(int fid)
        {
            var count = db.AirStopOvers.Where(m => m.FlightId == fid).ToList().Count;
            if (count >= 2)
                return false;
            return true;
        }
        [HttpPost]
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
                return RedirectToAction("FlightList");
            }
            else
            {
                return View("EditFlight", f);
            }
        }
        public ActionResult DeleteFlight(int id)
        {
            var f = db.Flights.Find(id);
            db.Entry(f).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("FlightList");
        }

        public ActionResult AddTicket()
        {
            ViewBag.ticketType = new SelectList(TicketTypeSelectList(), "value", "text");
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

        public ActionResult AddTempTicket()
        {
            ViewBag.ticketType = new SelectList(TicketTypeSelectList(), "value", "text");
            TicketTemp t = new TicketTemp();
            return View("AddTempTicket", t);
        }
        [HttpPost]
        public ActionResult AddTempTicket(TicketTemp t)
        {
            if (ModelState.IsValid)
            {
                t.Price = GetPrice((int)t.TypeTicketId);
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ticketType = new SelectList(TicketTypeSelectList(), "value", "text");
                return View("AddTempTicket", t);
            }

        }
        public ActionResult EditTempTicket(int tid)
        {
            Ticket t = db.Tickets.Find(tid);
            return View("AddTempTicket", t);
        }
        [HttpPost]
        public ActionResult EditTempTicket(Ticket t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditTempTicket", t);
            }
        }
        public ActionResult DeleteTempTicket(int tid)
        {
            var t = db.Tickets.Find(tid);
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddTicketType()
        {
            TypeTicket t = new TypeTicket();
            return View("AddTicketType", t);
        }
        [HttpPost]

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
                return View("AddTicketType", t);
            }

        }
        public ActionResult EditTypeTicket(int tid)
        {
            TypeTicket t = db.TypeTickets.Find(tid);
            return View("AddTicketType", t);
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
                return View("EditTicketType", t);
            }
        }
        public ActionResult DeleteTypeTicket(int tid)
        {
            var t = db.TypeTickets.Find(tid);
            db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<SelectListItem> AirportSelectListItem()
        {
            List<SelectListItem> sl = new List<SelectListItem>();
            var apl = AirportList();
            foreach(var item in apl)
            {
                SelectListItem ap = new SelectListItem();
                ap.Value = item.Id.ToString();
                ap.Text = item.Name;
                sl.Add(ap);
            }
            return sl;
        }

        public List<Airport> AirportList()
        {
            var apl = db.Airports.ToList();
            return apl;
        }
        public List<Plane> PlaneList()
        {
            return db.Planes.ToList();
        }
        public List<SelectListItem> PlaneSelectList()
        {
            List<SelectListItem> sl = new List<SelectListItem>();
            var apl = PlaneList();
            foreach (var item in apl)
            {
                SelectListItem ap = new SelectListItem();
                ap.Value = item.Id.ToString();
                ap.Text = item.Name;
                sl.Add(ap);
            }
            return sl;
        }

        public List<SelectListItem> TicketTypeSelectList()
        {
            List<SelectListItem> tpsl = new List<SelectListItem>();
            var tpl = db.TypeTickets.ToList();
            foreach(var item in tpl)
            {
                SelectListItem tp = new SelectListItem();
                tp.Value = item.Id.ToString();
                tp.Text = item.Name;
                tpsl.Add(tp);
            }
            return tpsl;
        }

        public decimal GetPrice(int typeid)
        {
            var tt = db.TypeTickets.Find(typeid);
            return (decimal)tt.Price;
        }

        public List<TicketTemp> GetTicketTempList()
        {
            return db.TicketTemps.ToList();
        }

        public List<Flight> GetFlightList()
        {
            return db.Flights.ToList();
        }
        public List<AirStopOver> AirStopList(int fid)
        {
            return db.AirStopOvers.Where(m => m.FlightId == fid).ToList();
        }
     
    }
}