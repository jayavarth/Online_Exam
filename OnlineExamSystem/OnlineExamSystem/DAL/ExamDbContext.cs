using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.DAL
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext()
            : base("OnlineExamDB")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Result> Results { get; set; }
    }
}