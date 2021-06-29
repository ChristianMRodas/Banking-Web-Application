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
    public class NotificationControllerTest : BaseControllerTest
    {
        ILogger<NotificationController> logger;
        DbContextOptions<BankingContext> options;
        Guid userId;
        IAuthenticationService authContext;
        INotificationRuleService notificationRuleService;

        [TestInitialize]
        public void Initialize()
        {
            // Create mock logger
            logger = Mock.Of<ILogger<NotificationController>>();
            // Create mock db options
            options = new DbContextOptionsBuilder<BankingContext>()
            .UseSqlite(CreateInMemoryDatabase())
            .Options;
            // Create userId
            userId = Guid.NewGuid();
            // Create mock authContext with GetUserId() method
            authContext = Mock.Of<IAuthenticationService>(auth => auth.GetUserId() == userId.ToString());

            notificationRuleService = Mock.Of<INotificationRuleService>();
        }

        [TestMethod]
        public void NotificationControllerRenders()
        {
            // Create BankingContext
            using (var bc = new BankingContext(options))
            {
                bc.Database.EnsureCreated();
            }
            // Create Fresh BankingContext
            using (var bc = new BankingContext(options))
            {
                // Create Controller
                NotificationController controller = new NotificationController(authContext, logger, bc, notificationRuleService);
                // Create resutls
                ViewResult result = controller.Notifications() as ViewResult;
                // Assert result renders
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public void RetrievesLoginNotification()
        {
            // Create BankingContext
            using (var bc = new BankingContext(options))
            {
                bc.Database.EnsureCreated();
                // Add a login notification
                bc.Notification.Add(new Notification
                {
                    UserId = userId,
                    Title = "Login",
                    Description = "Successfully logged into online banking",
                    NotifiedDate = DateTime.Now
                });
                bc.SaveChanges();
            }
            // Create Fresh BankingContext
            using (var bc = new BankingContext(options))
            {
                // Create Controller
                NotificationController controller = new NotificationController(authContext, logger, bc, notificationRuleService);
                // Create resutls
                ViewResult result = controller.Notifications() as ViewResult;
                // Assert result renders
                Assert.IsNotNull(result);
                // Assert Login Notification Count
                Assert.AreEqual(result.ViewData["LoginCount"], 1);
            }
        }
    }
}
