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
    public class TransactionControllerTest : BaseControllerTest
    {
        ILogger<TransactionController> logger;
        DbContextOptions<BankingContext> options;
        Guid userId;
        IAuthenticationService authContext;
        INotificationRuleService notificationRuleService;

        [TestInitialize]
        public void Initialize()
        {
            // Create mock logger
            logger = Mock.Of<ILogger<TransactionController>>();
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
        public void TransactionsRender()
        {
            using (var bc = new BankingContext(options))
            {
                bc.Database.EnsureCreated();
            }
            // Create Fresh BankingContext
            using (var bc = new BankingContext(options))
            {
                // Create Controller
                TransactionController controller = new TransactionController(authContext, logger, bc, notificationRuleService);
                // Create results
                ViewResult result = controller.Transactions() as ViewResult;
                // Assert result renders
                Assert.IsNotNull(result);
            }
        }
    }
}
