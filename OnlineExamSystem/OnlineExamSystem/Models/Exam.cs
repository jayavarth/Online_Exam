using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        public string ExamName { get; set; }

        public int LevelNo { get; set; }

        public int TotalQuestions { get; set; }

        public int PassMarks { get; set; }

        public int DurationMinutes { get; set; }
    }
}