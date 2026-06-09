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
            public int AdminId { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
        }
    
}