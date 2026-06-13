using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExamSystem.DAL;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.Controllers
{
    public class ExamController : Controller
    {
        ExamDbContext db = new ExamDbContext();
        public ActionResult SelectExam()
        {
            var exams = db.Exams.ToList();

            return View(exams);
        }
        public ActionResult Instructions(int id)
        {
            var exam = db.Exams.Find(id);

            return View(exam);
        }
        public ActionResult StartExam(int id)
        {
            int userId =
                Convert.ToInt32(Session["UserId"]);

            bool allowed =
                CanAttendLevel(userId, id);

            if (!allowed)
            {
                TempData["Message"] =
                    "You must pass previous level first.";

                return RedirectToAction("SelectExam");
            }

            Session["ExamId"] = id;

            var questions =
                db.Questions
                .Where(x => x.ExamId == id)
                .ToList();

            return View(questions);
        }
        [HttpPost]
        public ActionResult SubmitExam(FormCollection form)
        {
            int examId =
                Convert.ToInt32(
                    Session["ExamId"]);

            int userId =
                Convert.ToInt32(
                    Session["UserId"]);

            var questions =
                db.Questions
                .Where(x => x.ExamId == examId)
                .ToList();

            int score = 0;

            foreach (var q in questions)
            {
                string selectedAnswer =
                    form["Answer_" + q.QuestionId];

                if (selectedAnswer == q.CorrectAnswer)
                {
                    score++;
                }
            }

            var exam = db.Exams.Find(examId);

            Result result =
                new Result();

            result.UserId = userId;

            result.ExamId = examId;

            result.Score = score;

            result.Status =
                score >= exam.PassMarks
                ? "PASS"
                : "FAIL";

            result.ExamDate = DateTime.Now;

            db.Results.Add(result);

            db.SaveChanges();

            Session["ResultId"] =
                result.ResultId;

            return RedirectToAction(
                "ResultCard");
        }
        public ActionResult ResultCard()
        {
            int resultId =
                Convert.ToInt32(
                    Session["ResultId"]);

            var result =
                db.Results.Find(resultId);

            return View(result);
        }

        public ActionResult MyResults()
        {
            int userId =
                Convert.ToInt32(
                    Session["UserId"]);

            var results =
                db.Results
                .Where(x => x.UserId == userId)
                .ToList();

            return View(results);
        }
        public bool CanAttendLevel(int userId, int examId)
        {
            var currentExam = db.Exams.Find(examId);

            if (currentExam.LevelNo == 1)
                return true;

            var previousExam =
                db.Exams.FirstOrDefault(x =>
                    x.LevelNo == currentExam.LevelNo - 1);

            if (previousExam == null)
                return false;

            var passResult =
                db.Results.FirstOrDefault(x =>
                    x.UserId == userId &&
                    x.ExamId == previousExam.ExamId &&
                    x.Status == "PASS");

            return passResult != null;
        }
    }

}
