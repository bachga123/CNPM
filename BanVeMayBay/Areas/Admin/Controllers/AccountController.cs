﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeMayBay.Models;
using PagedList;
using BanVeMayBay.Areas.Admin.Data;
using System.Data.Entity;


namespace BanVeMayBay.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        CNPM_DHT_NHOM19Entities db = new CNPM_DHT_NHOM19Entities();
        // GET: Admin/Account
        public ActionResult Index(int page = 1,int pageSize = 10)
        {
            var list = AccountList().OrderByDescending(m => m.RoleId).ToPagedList(page, pageSize);
            return View(list);
        }
        public ActionResult AddAccount()
        {
            AddAccountForCustomer acc = new AddAccountForCustomer();
            return View(acc);
        }
        [HttpPost]
        public ActionResult AddAccount(AddAccountForCustomer re)
        {
            if (ModelState.IsValid)
            {
                var checkUsernameExists = db.Accounts.FirstOrDefault(m => m.UserName == re.Username);
                var checkEmailExists = db.Customers.FirstOrDefault(m => m.Email == re.Email);
                if (checkUsernameExists == null)
                {
                    if (checkEmailExists == null)
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
                        Models.Customer cus = new Models.Customer();
                        cus.Fname = re.FirstName;
                        cus.Lname = re.LastName;
                        cus.Email = re.Email;
                        cus.CreateDate = DateTime.Now;
                        cus.ModifyDate = DateTime.Now;
                        cus.AccountID = acc.Id;
                        db.Entry(cus).State = EntityState.Added;

                        // Save
                        db.SaveChanges();
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email has exists. Please try another email");
                        return View("AddAccount", re);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username has exists. Please try another username");
                    return View("AddAccount", re);
                }
            }
            else
            {
                return View("AddAccount", re);
            }
        }


        public ActionResult RemoveAccount(int accid)
        {
            var acc = db.Accounts.Find(accid);
            var cus = db.Customers.FirstOrDefault(m => m.AccountID == accid);
            db.Entry(cus).State = EntityState.Deleted;
            db.Entry(acc).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<Account> AccountList()
        {
            return db.Accounts.Where(m => m.RoleId == 0).ToList();
        }
    }
}