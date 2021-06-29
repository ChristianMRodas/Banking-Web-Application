using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommerceBankOnlineBanking.Models;
using CommerceBankOnlineBanking.Data;
using System.Web.Helpers;
using CommerceBankOnlineBanking.Controllers.Abstract;
using CommerceBankOnlineBanking.Services.Abstract;

namespace CommerceBankOnlineBanking.Controllers
{
    public class AuthenticationController : BaseRoute
    {
        private readonly BankingContext _context;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly INotificationRuleService _notificationRuleService;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger, BankingContext context, INotificationRuleService notificationRuleService) : base(authenticationService)
        {
            _logger = logger;
            _context = context;
            _notificationRuleService = notificationRuleService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("AuthUserId"))
            {
                Response.Cookies.Delete("AuthUserId");
            }
            return RedirectToAction("Login");
        }



        [HttpPost]
        public IActionResult Login(UserLoginViewModel userLogin)
        {
            if (userLogin.Username == null || userLogin.Username == "")
            {
                ViewData["Error"] = "Username must be provided";
                return View();
            }
            else if (userLogin.Password == null || userLogin.Password == "")
            {
                ViewData["Error"] = "Password must be provided";
                return View();
            }

            string username = userLogin.Username;
            string password = userLogin.Password;

            var user =
            from User in _context.Set<User>()
            where User.UserName == username
            select User;

            if (user.Count() == 0)
            {
                ViewData["Error"] = "Failed to sign in due to invalid username";
                return View();
            }

            var hashedPassword = user.First().Password;

            if (!Crypto.VerifyHashedPassword(hashedPassword, password))
            {
                ViewData["Error"] = "Failed to sign in due to wrong password";
                return View();
            }
            else
            {
                CreateAuthUserId(user.First().Id.ToString());
                if (CreateLoginNotification(user.First().Id))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Error"] = "Failed to create login notification";
                    return View();
                }
            }
        }




        // This method will be called when a user attempts to create an account.
        [HttpPost]
        public IActionResult SignUp(User newUser)
        {
            string lastName = newUser.FirstName;
            string firstName = newUser.LastName;
            string username = newUser.UserName;
            string password = newUser.Password;

            // Make sure username is unique. DB checks this also but we can fail more
            // gracefully if we catch it here.
            int unique = (from user in _context.Set<User>()
                          where user.UserName == username
                          select user).Count();
            if (unique != 0)
            {
                ViewData["Error"] = "Account creation failed. Username " +
                                    username + " is already in use.";
                return View("SignUp");
            }

            _context.Add(new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = username,
                Password = Crypto.HashPassword(password)
            });
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public bool CreateLoginNotification(Guid UserId)
        {
            bool loginNotificationEnabled = _notificationRuleService.GetNotificationRuleEnabled(UserId, NotificationRuleType.LOGIN);
            if (loginNotificationEnabled)
            {
                try
                {
                    _context.Add(new Notification
                    {
                        UserId = UserId,
                        Title = "Login",
                        Description = "Successfully logged into online banking",
                        NotifiedDate = DateTime.Now
                    });
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to create login notification for user {}", UserId);
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
    }



}
