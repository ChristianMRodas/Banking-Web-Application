using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommerceBankOnlineBanking.Controllers;
using CommerceBankOnlineBanking.Data;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CommerceBankOnlineBanking.Models;
using System;
using CommerceBankOnlineBanking.Services.Abstract;

namespace CommerceBankTest
{
    [TestClass]
    public class HomeControllerTest : BaseControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Create mock logger
            var logger = Mock.Of<ILogger<HomeController>>();
            // Create mock db options
            var options = new DbContextOptionsBuilder<BankingContext>()
            .UseSqlite(CreateInMemoryDatabase())
            .Options;
            // Create mock authContext with GetUserId() method
            var authContext = Mock.Of<IAuthenticationService>(auth => auth.GetUserId() == Guid.NewGuid().ToString());
            // Create BankingContext
            using (var bc = new BankingContext(options))
            {
                bc.Database.EnsureCreated();
                bc.Transaction.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    AccountNumber = 1,
                    Action = "Account Open",
                    Description = "Starting Balance",
                    ProcessingDate = System.DateTime.Parse("6/1/2020 8:00:00 AM"),
                    Balance = 5000.00,
                    Amount = 0
                });
                bc.SaveChanges();
            }
            // Create Fresh BankingContext
            using (var bc = new BankingContext(options))
            {
                // Create Controller
                HomeController controller = new HomeController(authContext, logger, bc);
                // Create resutls
                ViewResult result = controller.Index() as ViewResult;
                // Assert result renders
                Assert.IsNotNull(result);
            }
        }
    }
}
