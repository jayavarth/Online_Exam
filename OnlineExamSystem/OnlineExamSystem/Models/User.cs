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

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z ]+$",
            ErrorMessage = "Only alphabets allowed")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$",
            ErrorMessage =
            "Password must contain minimum 12 characters, 1 uppercase, 1 lowercase, 1 number and 1 special character")]
        public string Password { get; set; }

        [Required]
        [Compare("Password",
            ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Enter valid 10 digit mobile number")]
        public string Mobile { get; set; }

    //    //[Required]
    //    //public string City { get; set; }
    //    [Required(ErrorMessage = "City is required")]
    //    [RegularExpression(@"^[a-zA-Z ]+$",
    //ErrorMessage = "City should contain only letters")]
    //    public string City { get; set; }

    //    [Required(ErrorMessage = "State is required")]
    //    [RegularExpression(@"^[a-zA-Z ]+$",
    //ErrorMessage = "State should contain only letters")]
    //    public string State { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        //[Required]
        [Required]//public string Qualification { get; set; }
        public int CountryId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int QualificationId { get; set; }
        [Required]
        [Range(2000, 2100)]
        public int YearOfCompletion { get; set; }
        
    }
}