using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Mobile { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public DateTime DOB { get; set; }

        public string Qualification { get; set; }

        public int YearOfCompletion { get; set; }
    }
}