using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OnlineExamSystem.Models
{
   
        public class Admin
        {
            [Key]
        [Required]
            public int AdminId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        }
    
}