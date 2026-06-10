using OnlineExamSystem.DAL;
using OnlineExamSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlineExamSystem.Controllers
{
    public class AccountController : Controller
    {
        ExamDbContext db = new ExamDbContext();

        public ActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Register(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingUser =
        //        db.Users.FirstOrDefault(x => x.Email == user.Email);
        //        if (db.Users.Any(x => x.Email == user.Email))
        //        {
        //            ModelState.AddModelError(
        //                "Email",
        //                "Email already registered");
        //        }

        //        if (existingUser != null)
        //        {
        //            ViewBag.Message = "Email already exists";
        //            return View();
        //        }
        //        int age = DateTime.Now.Year - user.DOB.Year;

        //        if (age < 18)
        //        {
        //            ModelState.AddModelError(
        //                "DOB",
        //                "Age must be 18 or above");
        //        }
        //        db.Users.Add(user);
        //        db.SaveChanges();

        //        return RedirectToAction("Login");
        //    }

        //    return View();
        //}
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (db.Users.Any(x => x.Email == user.Email))
            {
                ModelState.AddModelError(
                    "Email",
                    "Email already exists");
            }

            if (db.Users.Any(x => x.Mobile == user.Mobile))
            {
                ModelState.AddModelError(
                    "Mobile",
                    "Mobile already exists");
            }

            int age = DateTime.Now.Year - user.DOB.Year;

            if (age < 18)
            {
                ModelState.AddModelError(
                    "DOB",
                    "Age must be 18+");
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                TempData["Success"] =
                    "Registration Successful";

                return RedirectToAction("Login");
            }

            return View(user);
        }

        //2
        public ActionResult Login()
        {
            Random random = new Random();

            int captcha = random.Next(1000, 9999);

            Session["Captcha"] = captcha;

            ViewBag.Captcha = captcha;

            return View();
        }
        [HttpPost]
        public ActionResult Login(
    string email,
    string password,
    string captchaInput)
        {
            // Captcha Validation
            if (captchaInput != Session["Captcha"]?.ToString())
            {
                ViewBag.Message = "Invalid Captcha";

                Random r = new Random();
                int captcha = r.Next(1000, 9999);

                Session["Captcha"] = captcha;
                ViewBag.Captcha = captcha;

                return View();
            }

            // Check Email Exists
            var user = db.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                ViewBag.Message = "Email is not registered";

                Random r = new Random();
                int captcha = r.Next(1000, 9999);

                Session["Captcha"] = captcha;
                ViewBag.Captcha = captcha;

                return View();
            }

            // Check Password
            if (user.Password != password)
            {
                ViewBag.Message = "Incorrect Password";

                Random r = new Random();
                int captcha = r.Next(1000, 9999);

                Session["Captcha"] = captcha;
                ViewBag.Captcha = captcha;

                return View();
            }

            Session["UserId"] = user.UserId;
            Session["UserName"] = user.FullName;

            return RedirectToAction("Dashboard");
        }
        //3
        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email, string newPassword)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                user.Password = newPassword;
                db.SaveChanges();

                ViewBag.Message = "Password Updated Successfully";
            }
            else
            {
                ViewBag.Message = "Email Not Found";
            }

            return View();
        }
    }
}