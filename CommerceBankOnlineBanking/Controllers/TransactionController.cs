using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommerceBankOnlineBanking.Models;
using CommerceBankOnlineBanking.Data;
using CommerceBankOnlineBanking.Controllers.Abstract;
using CommerceBankOnlineBanking.Services.Abstract;

namespace CommerceBankOnlineBanking.Controllers
{
    public class TransactionController : BaseRoute
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly BankingContext _context;
        private readonly INotificationRuleService _notificationRuleService;

        public TransactionController(IAuthenticationService authenticationService, ILogger<TransactionController> logger, BankingContext context, INotificationRuleService notificationRuleService) : base(authenticationService)
        {
            _logger = logger;
            _context = context;
            _notificationRuleService = notificationRuleService;
        }

        //Shows current transactions
        public IActionResult Transactions()
        {
            if (Authenticate() != -1)
            {
                String userId = GetUserId();
                var transactions = (from Transaction in _context.Set<Transaction>() where Transaction.UserId == System.Guid.Parse(userId) select Transaction).ToList();
                return View(transactions);
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpPost]
        public IActionResult addTransaction(Transaction transaction)
        {
            if (transaction != default(Transaction))
            {
                String UserId = GetUserId();
                Guid UserGuid = Guid.Parse(UserId);
                Transaction lastTransaction = GetLastTransaction();
                Double previousBalance = lastTransaction != default(Transaction) ? lastTransaction.Balance : 0;
                Double newBalance = previousBalance + transaction.Amount;

                _context.Add(new Transaction
                {
                    UserId = UserGuid,
                    AccountNumber = transaction.AccountNumber,
                    Action = transaction.Action,
                    Description = transaction.Description,
                    ProcessingDate = DateTime.Now,
                    Balance = newBalance,
                    Amount = transaction.Amount
                });

                _context.SaveChanges();
                CreateTransactionNotification(UserGuid, transaction.Amount);
                if (newBalance < 0)
                {
                    CreateNegativeBalanceNotification(UserGuid, newBalance);
                }
            }
            return RedirectToAction("Transactions", "Transaction");
        }

        public IActionResult AddTransaction()
        {
            if (Authenticate() != -1)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }


        }

        public Transaction GetLastTransaction()
        {
            String userId = GetUserId();
            var transactions = (from Transaction in _context.Set<Transaction>() where Transaction.UserId == System.Guid.Parse(userId) orderby Transaction.ProcessingDate descending select Transaction).ToList();
            Transaction lastTransaction = transactions.FirstOrDefault();
            return lastTransaction;
        }
        public bool CreateTransactionNotification(Guid UserId, double amount)
        {
            bool loginNotificationEnabled = _notificationRuleService.GetNotificationRuleEnabled(UserId, NotificationRuleType.TRANSACTION);
            if (loginNotificationEnabled)
            {
                try
                {
                    _context.Add(new Notification
                    {
                        UserId = UserId,
                        Title = NotificationRuleType.TRANSACTION,
                        Description = $"A transaction for amount ${amount} has been completed successfully",
                        NotifiedDate = DateTime.Now
                    });
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to create transaction notification for user {UserId}", UserId);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        public bool CreateNegativeBalanceNotification(Guid UserId, double balance)
        {
            bool loginNotificationEnabled = _notificationRuleService.GetNotificationRuleEnabled(UserId, NotificationRuleType.NEGATIVE_BALANCE);
            if (loginNotificationEnabled)
            {
                try
                {
                    _context.Add(new Notification
                    {
                        UserId = UserId,
                        Title = NotificationRuleType.NEGATIVE_BALANCE,
                        Description = $"You have a negative account balance of ${balance}",
                        NotifiedDate = DateTime.Now
                    });
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to create negative balance notification for user {UserId}", UserId);
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