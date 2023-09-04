using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelsLayer;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public static List<UsersModel> users { get; set; }
        public static List<DiariesModel> diaries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        static ApplicationDbContext()
        {
            
            users = new List<UsersModel>
            {
                new UsersModel()
                {
                    userId = -1,
                    userName = "default",
                    password = "1111",
                    name = "default",
                    surname = "default",
                    securityQuestion = "default",
                    securityAnswer = "default",
                    dateOfRegister = DateTime.Now
                }
            };

            diaries = new List<DiariesModel>
            {
                new DiariesModel()
                {
                    diaryId = -1,
                    diary = "default",
                    date = DateTime.Now,
                    updateDate = DateTime.Now,
                    userId = -1
                } 
            };
        }        
    }
}