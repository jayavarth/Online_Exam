using OfficeOpenXml;
using OnlineExamSystem.DAL;
using OnlineExamSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace OnlineExamSystem.Controllers
{
    public class AdminController : Controller
    {
        ExamDbContext db = new ExamDbContext();

        public ActionResult Login()
        { 
        
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
        public ActionResult ForgotPassword(string email)
        {

            var admin =
                db.Admins.FirstOrDefault(x => x.Email == email);

            if (admin == null)
            {
                ViewBag.Message = "Email Not Found";
                return View();
            }

            Random r = new Random();

            string otp = r.Next(100000, 999999).ToString();

            admin.OTP = otp;
            admin.OTPExpiry = DateTime.Now.AddMinutes(5);

            db.SaveChanges();

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("vilvapriya27@gmail.com");

            mail.To.Add(admin.Email);

            mail.Subject = "Password Reset OTP";

            mail.Body = "Your OTP is : " + otp;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials =
                new NetworkCredential(
                    "vilvapriya27@gmail.com",
                    "osopddmzkdbdomri");

            smtp.EnableSsl = true;

            smtp.Send(mail);

            Session["ResetEmail"] = email;

            return RedirectToAction("VerifyOTP");
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
        public ActionResult VerifyOTP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyOTP(string otp)
        {
            string admin =
                Session["ResetEmail"].ToString();

            var adminUser =
                db.Admins.FirstOrDefault(x => x.Email == admin);
            if (adminUser == null)
            {
                return RedirectToAction("ForgotPassword");
            }
                            
            if (adminUser.OTP != otp)
            {
                ViewBag.Message = "Invalid OTP";
                return View();
            }

            if (adminUser.OTPExpiry < DateTime.Now)
            {
                ViewBag.Message = "OTP Expired";
                return View();
            }

            return RedirectToAction("ResetPassword");
        }
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveQuestions(List<Question> questions)
        {
            try
            {
                if (questions == null || questions.Count == 0)
                {
                    return Json(new { success = false, message = "No questions received" });
                }

                foreach (var q in questions)
                {
                    if (string.IsNullOrEmpty(q.QuestionText) || string.IsNullOrEmpty(q.CorrectAnswer))
                    {
                        return Json(new { success = false, message = "Invalid question data" });
                    }

                    db.Questions.Add(q);
                }

                db.SaveChanges();

                return Json(new { success = true, message = "Questions saved successfully" });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error: " + ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(
            string newPassword,
            string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ViewBag.Message =
                    "Passwords do not match";

                return View();
            }

            string email =
                Session["ResetEmail"].ToString();

            //var user =
            //    db.Users.FirstOrDefault(x => x.Email == email);
            var admin =
    db.Admins.FirstOrDefault(x => x.Email == email);
            //user.Password = newPassword;

            //user.OTP = null;
            //user.OTPExpiry = null;
            admin.Password = newPassword;

            admin.OTP = null;
            admin.OTPExpiry = null;

            db.SaveChanges();

            //db.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}