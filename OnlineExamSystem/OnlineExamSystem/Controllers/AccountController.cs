using OnlineExamSystem.DAL;
using OnlineExamSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
namespace OnlineExamSystem.Controllers
{
    public class AccountController : Controller
    {
        ExamDbContext db = new ExamDbContext();

        public ActionResult Register()
        {
            ViewBag.Countries =
        new SelectList(
            db.Countries.ToList(),
            "CountryId",
            "CountryName");

            ViewBag.Qualifications =
                new SelectList(
                    db.Qualifications.ToList(),
                    "QualificationId",
                    "QualificationName");

            return View();
            //return View();
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
            if (user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError(
                    "ConfirmPassword",
                    "Passwords do not match");

                return View(user);
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

            //return View(user);
            ViewBag.Countries = new SelectList(
    db.Countries.ToList(),
    "CountryId",
    "CountryName",
    user.CountryId);

            ViewBag.Qualifications = new SelectList(
                db.Qualifications.ToList(),
                "QualificationId",
                "QualificationName",
                user.QualificationId);

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

        //[HttpPost]
        //public ActionResult ForgotPassword(string email, string newPassword)
        //{
        //    var user = db.Users.FirstOrDefault(x => x.Email == email);

        //    if (user != null)
        //    {
        //        user.Password = newPassword;
        //        db.SaveChanges();

        //        ViewBag.Message = "Password Updated Successfully";
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Email Not Found";
        //    }

        //    return View();
        //}
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var user =
                db.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                ViewBag.Message = "Email Not Found";
                return View();
            }

            Random r = new Random();

            string otp =
                r.Next(100000, 999999).ToString();

            user.OTP = otp;
            user.OTPExpiry =
                DateTime.Now.AddMinutes(5);
            ModelState.Remove("ConfirmPassword");
            //db.SaveChanges();
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityErrors.ValidationErrors)
                    {
                        ViewBag.Message =
                            validationError.PropertyName +
                            " : " +
                            validationError.ErrorMessage;
                    }
                }

                return View();
            }

            //    MailMessage mail =
            //        new MailMessage();

            //    mail.From =
            //        new MailAddress("vilvapriya27@gmail.com");

            //    //mail.To.Add(user.Email);
            //    mail.To.Add("vilvamani27@gmail.com");


            //    mail.Subject = "Password Reset OTP";

            //    mail.Body =
            //        "Your OTP is : " + otp;
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.Host = "smtp.gmail.com";
            //    smtp.Port = 465;
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = false;

            //    smtp.Credentials =
            //        new NetworkCredential(
            //            "vilvapriya27@gmail.com",
            //            "nmkgholyompnjkkr");
            //   // SmtpClient smtp =
            //   //new SmtpClient("smtp.gmail.com", 587);

            //    //smtp.Credentials =
            //    //    new NetworkCredential(
            //    //        "vilvapriya27@gmail.com",
            //    //        "nmkgholyompnjkkr");

            //    smtp.EnableSsl = true;

            //    //smtp.Send(mail);
            //    try
            //    {
            //        smtp.Send(mail);

            //        ViewBag.Message = "OTP Sent Successfully";
            //    }
            //    catch (Exception ex)
            //    {
            //        ViewBag.Message = ex.ToString();

            //        return View();
            //    }

            //    Session["ResetEmail"] = email;

            //    return RedirectToAction("VerifyOTP");
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("vilvapriya27@gmail.com");
            //mail.To.Add("vilvamani27@gmail.com");
            mail.To.Add(user.Email);

            mail.Subject = "Password Reset OTP";
            mail.Body = "Your OTP is : " + otp;

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials =
                new NetworkCredential(
                    "vilvapriya27@gmail.com",
                    "osopddmzkdbdomri");

            smtp.Timeout = 60000;

            //smtp.Send(mail);
            try
            {
                System.Net.ServicePointManager.SecurityProtocol =
    SecurityProtocolType.Tls12;
                ViewBag.Message = "Trying to send...";
                smtp.Send(mail);

                ViewBag.Message = "OTP Sent Successfully";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();

                return View();
            }

            Session["ResetEmail"] = email;

            return RedirectToAction("VerifyOTP");
        }


        public ActionResult VerifyOTP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VerifyOTP(string otp)
        {
            string email =
                Session["ResetEmail"].ToString();

            var user =
                db.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return RedirectToAction("ForgotPassword");
            }

            if (user.OTP != otp)
            {
                ViewBag.Message = "Invalid OTP";
                return View();
            }

            if (user.OTPExpiry < DateTime.Now)
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

            var user =
                db.Users.FirstOrDefault(x => x.Email == email);

            user.Password = newPassword;

            user.OTP = null;
            user.OTPExpiry = null;

            //db.SaveChanges();
            try
            {
                db.SaveChanges();
                //return RedirectToAction("PasswordResetSuccess");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityErrors.ValidationErrors)
                    {
                        ViewBag.Message =
                            validationError.PropertyName +
                            " : " +
                            validationError.ErrorMessage;
                    }
                }

                return View();
            }

            //return RedirectToAction("Login");
            return RedirectToAction("PasswordResetSuccess");


        }
        public ActionResult PasswordResetSuccess()
        {
            return View();
        }
        public JsonResult GetStates(int countryId)
        {
            var states =
                db.States
                .Where(x => x.CountryId == countryId)
                .ToList();

            return Json(
                states,
                JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCities(int stateId)
        {
            var cities =
                db.Cities
                .Where(x => x.StateId == stateId)
                .ToList();

            return Json(
                cities,
                JsonRequestBehavior.AllowGet);
        }
    }
}