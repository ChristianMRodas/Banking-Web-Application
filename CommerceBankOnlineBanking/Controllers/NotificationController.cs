using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommerceBankOnlineBanking.Models;
using CommerceBankOnlineBanking.Data;
using CommerceBankOnlineBanking.Controllers.Abstract;
using CommerceBankOnlineBanking.Services.Abstract;

//API for Excel Exporter
using ClosedXML.Excel;


namespace CommerceBankOnlineBanking.Controllers
{
    public class NotificationController : BaseRoute
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly BankingContext _context;
        private readonly INotificationRuleService _notificationRuleService;


        public NotificationController(IAuthenticationService authenticationService, ILogger<NotificationController> logger, BankingContext context, INotificationRuleService notificationRuleService) : base(authenticationService)
        {
            _logger = logger;
            _context = context;
            _notificationRuleService = notificationRuleService;
        }

        public IActionResult Notifications()
        {
            if (Authenticate() != -1)
            {
                String userId = GetUserId();
                Guid userIdGuid = Guid.Parse(userId);
                bool loginEnabled = _notificationRuleService.GetNotificationRuleEnabled(userIdGuid, NotificationRuleType.LOGIN);
                bool transactionEnabled = _notificationRuleService.GetNotificationRuleEnabled(userIdGuid, NotificationRuleType.TRANSACTION);
                bool balanceEnabled = _notificationRuleService.GetNotificationRuleEnabled(userIdGuid, NotificationRuleType.NEGATIVE_BALANCE);
                ViewData["LoginEnabled"] = loginEnabled;
                ViewData["TransactionEnabled"] = transactionEnabled;
                ViewData["BalanceEnabled"] = balanceEnabled;
                var notifications = (from Notification in _context.Set<Notification>() where Notification.UserId == Guid.Parse(userId) orderby Notification.NotifiedDate descending select Notification).ToList();
                var loginCount = (from Notification in _context.Set<Notification>() where (Notification.UserId == Guid.Parse(userId) && Notification.Title == NotificationRuleType.LOGIN) select Notification).Count();
                ViewData["LoginCount"] = loginCount > 0 ? loginCount : 0;
                var transactionCount = (from Notification in _context.Set<Notification>() where (Notification.UserId == Guid.Parse(userId) && Notification.Title == NotificationRuleType.TRANSACTION) select Notification).Count();
                ViewData["TransactionCount"] = transactionCount > 0 ? transactionCount : 0;
                var balanceCount = (from Notification in _context.Set<Notification>() where (Notification.UserId == Guid.Parse(userId) && Notification.Title == NotificationRuleType.NEGATIVE_BALANCE) select Notification).Count();
                ViewData["BalanceCount"] = balanceCount > 0 ? balanceCount : 0;
                return View(notifications);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }
        public IActionResult SetRuleType(string ruleType, bool enabled)
        {
            String userId = GetUserId();
            Guid userIdGuid = Guid.Parse(userId);
            if (enabled)
            {
                _notificationRuleService.EnableNotificationRule(userIdGuid, ruleType);
                return RedirectToAction("Notifications");
            }
            else
            {
                _notificationRuleService.DisableNotificationRule(userIdGuid, ruleType);
                return RedirectToAction("Notifications");
            }

        }

        //Exports user's notifications to an Excel Worksheet via automatic download
        [HttpPost]
        public ActionResult ExportNotifications()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "NotificationInformation" + DateTime.Now.Year.ToString() + ".xls";

            List<Notification> obj = new List<Notification>();

            var workbook = new XLWorkbook();

            //build workbook
            IXLWorksheet worksheet = workbook.Worksheets.Add("Notifications");
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "UserId";
            worksheet.Cell(1, 3).Value = "Title";
            worksheet.Cell(1, 4).Value = "Description";
            worksheet.Cell(1, 5).Value = "NotifiedDate";

            obj = NotificationInfo();

            int index = 2;
            foreach (Notification val in obj)
            {
                worksheet.Cell(index, 1).Value = val.Id.ToString();
                worksheet.Cell(index, 2).Value = val.UserId.ToString();
                worksheet.Cell(index, 3).Value = val.Title.ToString();
                worksheet.Cell(index, 4).Value = val.Description.ToString();
                worksheet.Cell(index, 5).Value = val.NotifiedDate.ToString();
                index++;
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            }
        }

        //Adds values from the notification model to the excel worksheet
        public List<Notification> NotificationInfo()
        {
            List<Notification> notificationobj; //= new List<Notification>();
            try
            {
                String userId = GetUserId();

                var notifications = (from Notification in _context.Set<Notification>() where Notification.UserId == Guid.Parse(userId) select Notification).ToList();

                notificationobj = notifications;
            }
            catch
            {
                notificationobj = new List<Notification>();
            }
            return notificationobj;
        }
    }
}




