using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        public int UserId { get; set; }

        public int ExamId { get; set; }

        public int Score { get; set; }

        public string Status { get; set; }

        public DateTime ExamDate { get; set; }
    }
}