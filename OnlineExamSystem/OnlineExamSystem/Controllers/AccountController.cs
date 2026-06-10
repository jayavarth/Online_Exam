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

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser =
                    db.Users.FirstOrDefault(x => x.Email == user.Email);

                if (existingUser != null)
                {
                    ViewBag.Message = "Email already exists";
                    return View();
                }

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View();
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
            if (captchaInput != Session["Captcha"].ToString())
            {
                ViewBag.Message = "Invalid Captcha";

                Random random = new Random();
                int captcha = random.Next(1000, 9999);

                Session["Captcha"] = captcha;
                ViewBag.Captcha = captcha;

                return View();
            }

            var user =
                db.Users.FirstOrDefault(
                    x => x.Email == email &&
                         x.Password == password);

            if (user != null)
            {
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.FullName;

                return RedirectToAction("Dashboard");
            }

            ViewBag.Message = "Invalid Login";

            Random r = new Random();
            int c = r.Next(1000, 9999);

            Session["Captcha"] = c;
            ViewBag.Captcha = c;

            return View();
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