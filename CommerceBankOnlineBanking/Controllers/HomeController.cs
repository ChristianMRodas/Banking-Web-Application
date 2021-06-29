using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommerceBankOnlineBanking.Models;
using CommerceBankOnlineBanking.Data;
using CommerceBankOnlineBanking.Controllers.Abstract;
using CommerceBankOnlineBanking.Services.Abstract;

namespace CommerceBankOnlineBanking.Controllers
{
    public class HomeController : BaseRoute
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BankingContext _context;
        public HomeController(IAuthenticationService authenticationService, ILogger<HomeController> logger, BankingContext context) : base(authenticationService)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (Authenticate() != -1)
            {
                String userId = GetUserId();
                var transactions = (from Transaction in _context.Set<Transaction>() where Transaction.UserId == System.Guid.Parse(userId) select Transaction).ToList();
                ViewData["Balance"] = "N/A";
                ViewData["Date"] = "N/A";
                try
                {
                    var lastTransaction = transactions.Last();
                    ViewData["Balance"] = lastTransaction.Balance.ToString();
                    ViewData["Date"] = lastTransaction.ProcessingDate.ToString();
                    _logger.LogInformation("Sucessfully retreived last transaction for user {}", userId);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to retreive last transaction for user {}", userId);
                }
                var notifications = (from Notification in _context.Set<Notification>() where Notification.UserId == Guid.Parse(userId) select Notification).ToList();
                return View(transactions);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Notifications()
        {
            if (Authenticate() != -1)
            {
                Request.Cookies.TryGetValue("AuthUserId", out String userId);
                var notifications = (from Notification in _context.Set<Notification>() where Notification.UserId == Guid.Parse(userId) select Notification).ToList();
                return View(notifications);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        


    }
}


