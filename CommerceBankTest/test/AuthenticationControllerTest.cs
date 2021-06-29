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
using System.Web.Helpers;
using System.Linq;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace CommerceBankTest
{
    [TestClass]
    public class AuthenticationControllerTest : BaseControllerTest
    {
        ILogger<AuthenticationController> logger;
        DbContextOptions<BankingContext> options;
        Guid userId;
        IAuthenticationService authContext;
        INotificationRuleService notificationRuleService;

        [TestInitialize]
        public void Initialize()
        {
            // Create mock logger
            logger = Mock.Of<ILogger<AuthenticationController>>();
            // Create mock db options
            options = new DbContextOptionsBuilder<BankingContext>().UseSqlite(CreateInMemoryDatabase()).Options;
            // Create userId
            userId = Guid.NewGuid();
            // Create mock authContext with GetUserId() method
            authContext = Mock.Of<IAuthenticationService>(auth => auth.GetUserId() == userId.ToString());
            // Create mock notificationRuleService with GetNotificationRuleEnabled() method
            // Login Notification Rule is Enabled
            notificationRuleService = Mock.Of<INotificationRuleService>(service => service.GetNotificationRuleEnabled(It.IsAny<Guid>(), NotificationRuleType.LOGIN) == true);
        }



        [TestMethod]
        public void LoginRenders()
        {

            // Create BankingContext
            using (var bc = new BankingContext(options))
            {
                bc.Database.EnsureCreated();
                // Add a User
                bc.User.Add(new User()
                {
                    Id = userId,
                    FirstName = "Admin",
                    LastName = "Pass",
                    UserName = "AdminPass1",
                    Password = Crypto.HashPassword("P@ssw0rd")
                });
                bc.SaveChanges();
            }

            // Create Fresh BankingContext
            using (var bc = new BankingContext(options))
            {
                // Create Controller
                AuthenticationController controller = new AuthenticationController(authContext, logger, bc, notificationRuleService);
                // Create resutls
                ViewResult result = controller.Login() as ViewResult;
                // Assert result renders
                Assert.IsNotNull(result);
            }
        }



        //Test for successful login
        [TestMethod]
        public void SuccessfulLogin() {
            
            User user = new User()
            {
                Id = userId,
                FirstName = "Admin_test",
                LastName = "Pass_test",
                UserName = "AdminPass1_test",
                Password = Crypto.HashPassword("P@ssw0rd_test")
            };


            // Create BankingContext
            using (var bc = new BankingContext(options))
            {
                bc.Database.EnsureCreated();
                // Add a User
                bc.User.Add(user);
                bc.SaveChanges();
            }
            // Create Fresh BankingContext
            using (var bc = new BankingContext(options))
            {
                // Create Controller
                AuthenticationController controller = new AuthenticationController(authContext, logger, bc, notificationRuleService);
                // Create Login Model
                UserLoginViewModel loginModel = new UserLoginViewModel
                {
                    Username = user.UserName,
                    Password = "P@ssw0rd_test"
                };


                // Create results
                RedirectToActionResult result = controller.Login(loginModel) as RedirectToActionResult;

                // Assert result renders
                Assert.IsNotNull(result);
                // Assert Successful login
                Assert.AreEqual("Index", result.ActionName);
                Assert.AreEqual("Home", result.ControllerName);

                // Clear resources
                bc.Dispose();
            }

        }



        [TestMethod]
        public void SucessfullyTriggersLoginNotification()
        {
            User user = new User()
            {
                Id = userId,
                FirstName = "Admin",
                LastName = "Pass",
                UserName = "AdminPass1",
                Password = Crypto.HashPassword("P@ssw0rd")
            };



            // Create BankingContext
            using (var bc = new BankingContext(options))
            {
                bc.Database.EnsureCreated();
                // Add a User
                bc.User.Add(user);
                bc.SaveChanges();
            }
            
            // Create Fresh BankingContext
            using (var bc = new BankingContext(options))
            {
                // Create Controller
                AuthenticationController controller = new AuthenticationController(authContext, logger, bc, notificationRuleService);
                // Create Login Model
                UserLoginViewModel loginModel = new UserLoginViewModel
                {
                    Username = user.UserName,
                    Password = "P@ssw0rd"
                };


                // Create resutls
                RedirectToActionResult result = controller.Login(loginModel) as RedirectToActionResult;
                // Assert result renders
                Assert.IsNotNull(result);
                // Assert login notification is triggered
                var loginNotifications = bc.Notification.Where((notification) => notification.UserId == user.Id && notification.Title == "Login");
                Assert.AreEqual(1, loginNotifications.Count());
                // Assert Successful login
                Assert.AreEqual("Index", result.ActionName);
                Assert.AreEqual("Home", result.ControllerName);
                // Clear resources
                bc.Dispose();
            }
        }

    }
}
