using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExamSystem.DAL;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.Controllers
{
    public class AdminController : Controller
    {
        ExamDbContext db = new ExamDbContext();

        public ActionResult Login()
        {
            Random random = new Random();

            int captcha = random.Next(1000, 9999);

            Session["AdminCaptcha"] = captcha;

            ViewBag.Captcha = captcha;

            return View();
        }

        //    [HttpPost]
        //    public ActionResult Login(
        //string email,
        //string password,
        //string captchaInput)
        //    {
        //        if (captchaInput != Session["AdminCaptcha"].ToString())
        //        {
        //            ViewBag.Message = "Invalid Captcha";

        //            Random random = new Random();

        //            int captcha = random.Next(1000, 9999);

        //            Session["AdminCaptcha"] = captcha;

        //            ViewBag.Captcha = captcha;

        //            return View();
        //        }

        //        var admin =
        //            db.Admins.FirstOrDefault(
        //                x => x.Email == email &&
        //                     x.Password == password);

        //        if (admin != null)
        //        {
        //            Session["Admin"] = admin.Email;

        //            return RedirectToAction("Dashboard");
        //        }

        //        ViewBag.Message = "Invalid Login";

        //        Random r = new Random();

        //        int c = r.Next(1000, 9999);

        //        Session["AdminCaptcha"] = c;

        //        ViewBag.Captcha = c;

        //        return View();
        //    }
        [HttpPost]
        public ActionResult Login(
        string email,
        string password,
        string captchaInput)
        {
            // Captcha Validation
            if (captchaInput != Session["AdminCaptcha"]?.ToString())
            {
                ViewBag.Message = "Invalid Captcha";
                return View();
            }

            // Check Email Exists
            var admin = db.Admins.FirstOrDefault(x => x.Email == email);

            if (admin == null)
            {
                ViewBag.Message = "Admin Email Not Found";
                return View();
            }

            // Check Password
            if (admin.Password != password)
            {
                ViewBag.Message = "Incorrect Password";
                return View();
            }

            // Successful Login
            Session["Admin"] = admin.Email;

            return RedirectToAction("Dashboard");
        }
        public ActionResult Dashboard()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");

            return View();
        }

        public ActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(Question question)
        {
            db.Questions.Add(question);

            db.SaveChanges();

            ViewBag.Message = "Question Added Successfully";

            return View();
        }
        public ActionResult ViewQuestions()
        {
            var questions = db.Questions.ToList();

            return View(questions);
        }
        public ActionResult DeleteQuestion(int id)
        {
            var question = db.Questions.Find(id);

            db.Questions.Remove(question);

            db.SaveChanges();

            return RedirectToAction("ViewQuestions");
        }
        public ActionResult ViewUsers()
        {
            var users = db.Users.ToList();

            return View(users);
        }
        public ActionResult ViewResults()
        {
            var results = db.Results.ToList();

            return View(results);
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email, string newPassword)
        {
            var admin = db.Admins.FirstOrDefault(x => x.Email == email);

            if (admin != null)
            {
                admin.Password = newPassword;
                db.SaveChanges();

                ViewBag.Message = "Password Updated";
            }
            else
            {
                ViewBag.Message = "Admin Not Found";
            }

            return View();
        }
    }
}