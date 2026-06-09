using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExamSystem.DAL;

namespace OnlineExamSystem.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            ExamDbContext db = new ExamDbContext();

            int count = db.Users.Count();

            ViewBag.Count = count;

            return View();
        }
    }
}
