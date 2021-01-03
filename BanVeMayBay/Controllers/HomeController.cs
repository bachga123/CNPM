using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeMayBay.Models;
using System.Data.Entity;

namespace BanVeMayBay.Controllers
{
    public class HomeController : Controller
    {
        CNPM_DHT_NHOM19Entities db = new CNPM_DHT_NHOM19Entities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            LoginModel lg = new LoginModel();
            return View("Login",lg) ;
        }
        [HttpPost]
        public ActionResult Login(LoginModel lg)
        {
            if(ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(m => m.UserName == lg.Username && m.PassWord == lg.Password);
                if(check != null)
                {
                    if (check.RoleId == 1)
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    else if(check.RoleId == 2)
                        return RedirectToAction("Index", "Employee", new { area = "Employee" });
                    else
                        return RedirectToAction("Index", "Customer", new { area = "Customer" });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or password");
                    return View("Login", lg);
                }
            }
            else
            {
                ModelState.AddModelError("", "Error when login ! Please try again.");
                return View("Login", lg);
            }
        }
        public ActionResult Register()
        {
            RegisterModel re = new RegisterModel();
            return View(re);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel re)
        {
            if(ModelState.IsValid)
            {
                if(re.Password != re.RepeatPassword)
                {
                    ModelState.AddModelError("", "Repeat password not same.");
                    return View("Register", re);
                }
                var checkUsernameExists = db.Accounts.FirstOrDefault(m => m.UserName == re.Username);
                var checkEmailExists = db.Customers.FirstOrDefault(m => m.Email == re.Email);
                if(checkUsernameExists == null)
                {
                    if(checkEmailExists == null)
                    {
                        //Create account
                        Account acc = new Account();
                        acc.UserName = re.Username;
                        acc.PassWord = re.Password;
                        acc.CreateDate = DateTime.Now;
                        acc.ModifyDate = DateTime.Now;
                        acc.Active = false;
                        acc.ActivePasswordCode = Guid.NewGuid().ToString();
                        acc.RoleId = 0;
                        db.Entry(acc).State = EntityState.Added;
                        //Create Customer
                        Customer cus = new Customer();
                        cus.Fname = re.FirstName;
                        cus.Lname = re.LastName;
                        cus.Email = re.Email;
                        cus.CreateDate = DateTime.Now;
                        cus.ModifyDate = DateTime.Now;
                        cus.AccountID = acc.Id;
                        db.Entry(cus).State = EntityState.Added;

                        // Save
                        db.SaveChanges();
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email has exists. Please try another email");
                        return View("Register", re);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username has exists. Please try another username");
                    return View("Register", re);
                }
            }
            else
            {
                return View("Register", re);
            }
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}