using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeMayBay.Models;
using PagedList;
namespace BanVeMayBay.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        CNPM_DHT_NHOM19Entities db = new CNPM_DHT_NHOM19Entities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

    }
}