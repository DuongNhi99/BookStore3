using BookStore.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookStore.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        CSDLContext db = new CSDLContext();
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Include = "HoTen, Age, Email, Password ")] User register)
        {

            var check = db.Users.Count();
            var user = db.Users.FirstOrDefault(x => x.Email == register.Email);
            var checkemail = db.Users.ToList();
            if (user == null)
            {
                User account = new User();
                account.HoTen = register.HoTen;
                //account.SDT = register.SDT;
                //account.DiaChi = register.DiaChi;
                account.Age = register.Age;
                account.Email = register.Email;
                account.Password = register.Password;
                account.Ranking = 1;
                account.DiemTichLuy = 0;

                if (check == 0)
                {
                    account.MaQuyen = 1;
                }
                else
                {
                    account.MaQuyen = 2;
                }

                db.Users.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else if (register.Email == user.Email)
            {
                ViewBag.error = "Email already exists";
                return View();
            }

            return View();
        }

        //Chức năng và giao diện đăng nhập
        public ActionResult Login(string loi)
        {
            ViewBag.loi = loi;
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "Email, Password")] User user)
        {
            var accounts = db.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (accounts == null)
            {
                return RedirectToAction("Login", "Authentication", new { loi = "Sai thông tin đăng nhập" });
            }

            else if (user.Email == accounts.Email && user.Password == accounts.Password)
            {
                user.Email = accounts.Email;
                FormsAuthentication.SetAuthCookie(user.Email, false);
                if (accounts.MaQuyen == 1)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Info(string error)
        {
            var users = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name );
            ViewBag.error = error;
            return View(users);
        }
        public ActionResult EditHoTen(int id, string HoTen)
        {
            var us = db.Users.FirstOrDefault(x => x.IDUser == id);
            us.HoTen = HoTen;
            db.Entry(us).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Info");
        }
        public ActionResult EditAge(int id, int Age)
        {
            var us = db.Users.FirstOrDefault(x => x.IDUser == id);
            us.Age = Age;
            db.Entry(us).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Info");
        }
        public ActionResult EditEmail(int id, string Email)
        {
            var us = db.Users.FirstOrDefault(x => x.IDUser == id);
            us.Email = Email;
            db.Entry(us).State = EntityState.Modified;
            db.SaveChanges();
            FormsAuthentication.SetAuthCookie(us.Email, false);
            return RedirectToAction("Info");
        }
        public ActionResult EditPass(int id, string Password, string PasswordNew)
        {
            var us = db.Users.FirstOrDefault(x => x.IDUser == id);
            if (Password == us.Password)
            {
                us.Password = PasswordNew;
                db.Entry(us).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Info");
            }
            else
            {
                return RedirectToAction("Info", new { error = "Mật khẩu sai" });
            }
        }
    }
}