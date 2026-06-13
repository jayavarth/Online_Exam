using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExamSystem.DAL;
using OnlineExamSystem.Models;
using OfficeOpenXml;
using System.IO;

namespace OnlineExamSystem.Controllers
{
    public class AdminController : Controller
    {
        ExamDbContext db = new ExamDbContext();

        public ActionResult Login()
        { 
        //{
        //    Random random = new Random();

        //    int captcha = random.Next(1000, 9999);

        //    Session["AdminCaptcha"] = captcha;

        //    ViewBag.Captcha = captcha;

        //    return View();
      
            GenerateCaptcha();
            return View();
        
        }

        
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
                GenerateCaptcha();
                return View();
            }

            // Check Email Exists
            var admin = db.Admins.FirstOrDefault(x => x.Email == email);

            if (admin == null)
            {
                ViewBag.Message = "Admin Email Not Found";
                GenerateCaptcha();
                return View();
            }

            // Check Password
            if (admin.Password != password)
            {
                ViewBag.Message = "Incorrect Password";
                GenerateCaptcha();
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
        public ActionResult UploadQuestions()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadQuestions(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                ExcelPackage.License.SetNonCommercialPersonal("Vilvapriya");
                using (var package =
                    new ExcelPackage(file.InputStream))
                {
                    ExcelWorksheet sheet =
                        package.Workbook.Worksheets[0];

                    int rows =
                        sheet.Dimension.Rows;

                    for (int row = 2;
                         row <= rows;
                         row++)
                    {
                        Question q =
                            new Question();

                        q.ExamId =
                            Convert.ToInt32(
                                sheet.Cells[row, 1].Value);

                        q.QuestionText =
                            sheet.Cells[row, 2].Value?.ToString();

                        q.OptionA =
                            sheet.Cells[row, 3].Value?.ToString();

                        q.OptionB =
                            sheet.Cells[row, 4].Value?.ToString();

                        q.OptionC =
                            sheet.Cells[row, 5].Value?.ToString();

                        q.OptionD =
                            sheet.Cells[row, 6].Value?.ToString();

                        q.CorrectAnswer =
                            sheet.Cells[row, 7].Value?.ToString();

                        db.Questions.Add(q);
                    }

                    db.SaveChanges();
                }

                ViewBag.Message =
                    "Questions Uploaded Successfully";
            }

            return View();
        }
        private void GenerateCaptcha()
        {
            Random random = new Random();

            int captcha = random.Next(1000, 9999);

            Session["AdminCaptcha"] = captcha;

            ViewBag.Captcha = captcha;
        }
    }
}