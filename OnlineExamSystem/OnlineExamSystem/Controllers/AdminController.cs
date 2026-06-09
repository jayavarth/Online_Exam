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
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var admin = db.Admins.FirstOrDefault
            (
                x => x.Email == email &&
                     x.Password == password
            );

            if (admin != null)
            {
                Session["Admin"] = admin.Email;

                return RedirectToAction("Dashboard");
            }

            ViewBag.Message = "Invalid Login";

            return View();
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
    }
}