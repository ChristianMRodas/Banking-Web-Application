using Microsoft.EntityFrameworkCore;
using CommerceBankOnlineBanking.Models;
using System.Web.Helpers;

namespace CommerceBankOnlineBanking.Data
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> User { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationRule> NotificationRule { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var TestId = System.Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = System.Guid.NewGuid(),
                    FirstName = "Admin",
                    LastName = "Pass",
                    UserName = "AdminPass",
                    Password = Crypto.HashPassword("P@ssw0rd")
                },
            new User()
            {
                Id = TestId,
                FirstName = "Test",
                LastName = "User",
                UserName = "TestUser",
                Password = Crypto.HashPassword("TestP@ssw0rd")
            });

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction()
                {
                    Id = System.Guid.NewGuid(),
                    UserId = TestId,
                    AccountNumber = 1,
                    Action = "Account Open",
                    Description = "Starting Balance",
                    ProcessingDate = System.DateTime.Parse("6/1/2020 8:00:00 AM"),
                    Balance = 5000.00,
                    Amount = 0
                }
            );
        }
    }
}