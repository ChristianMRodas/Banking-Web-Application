using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CommerceBankOnlineBanking.Services.Abstract;
using System.Web;
using CommerceBankOnlineBanking.Data;
using CommerceBankOnlineBanking.Models;

namespace CommerceBankOnlineBanking.Services
{
    public class NotificationRuleService : INotificationRuleService
    {
        private readonly BankingContext _context;

        private ILogger<NotificationRuleService> _logger;
        public NotificationRuleService(ILogger<NotificationRuleService> logger, BankingContext context)
        {
            _context = context;
            _logger = logger;
        }


        // Set Enabled of a notification rule to true
        // Returns whether it was successfully enabled
        public bool EnableNotificationRule(Guid userId, string notificationRule)
        {
            if (!GetNotificationRuleEnabled(userId, notificationRule))
            {
                try
                {
                    NotificationRule rule = GetNotificationRules(userId, notificationRule).FirstOrDefault();
                    if (rule != default(NotificationRule))
                    {
                        rule.Enabled = true;
                    }
                    else
                    {
                        _context.NotificationRule.Add(new NotificationRule
                        {
                            Id = Guid.NewGuid(),
                            UserId = userId,
                            RuleType = notificationRule,
                            Enabled = true
                        });
                    }
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to enabled notification rule for user {} with ruleType {}");
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        // Set Enabled of a notification rule to false
        // Returns whether it was successfully disabled
        public bool DisableNotificationRule(Guid userId, string notificationRule)
        {
            if (GetNotificationRuleEnabled(userId, notificationRule))
            {
                try
                {
                    NotificationRule rule = GetNotificationRules(userId, notificationRule).FirstOrDefault();
                    if (rule != default(NotificationRule))
                    {
                        rule.Enabled = false;
                    }
                    else
                    {
                        _context.NotificationRule.Add(new NotificationRule
                        {
                            Id = Guid.NewGuid(),
                            UserId = userId,
                            RuleType = notificationRule,
                            Enabled = false
                        });
                    }
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to disable notification rule for user {userId} with ruleType {notificationRule}", userId, notificationRule);
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        // Returns the enabled value of a notification rule
        // Defaults to true
        // throws Exception if more than one notification rule is found
        public bool GetNotificationRuleEnabled(Guid userId, string ruleType)
        {
            try
            {
                IEnumerable<NotificationRule> notificationRules = GetNotificationRules(userId, ruleType);
                if (notificationRules.Count() == 1)
                {
                    return notificationRules.First().Enabled;
                }
                else if (notificationRules.Count() > 1)
                {
                    throw new Exception($"More than one notification rules found for user {userId} and ruleType {ruleType}");
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get notification rules for user {} and ruleType {}", userId, ruleType);
                return true;
            }
        }

        private IEnumerable<NotificationRule> GetNotificationRules(Guid userId, string ruleType)
        {
            switch (ruleType)
            {
                case (NotificationRuleType.LOGIN):
                    var loginNotificationsRules = (from notificationRule in _context.Set<NotificationRule>() where notificationRule.UserId == userId && notificationRule.RuleType == NotificationRuleType.LOGIN select notificationRule).ToList();
                    return loginNotificationsRules;
                case (NotificationRuleType.TRANSACTION):
                    var transactionNotificationsRules = (from notificationRule in _context.Set<NotificationRule>() where notificationRule.UserId == userId && notificationRule.RuleType == NotificationRuleType.TRANSACTION select notificationRule).ToList();
                    return transactionNotificationsRules;
                case (NotificationRuleType.NEGATIVE_BALANCE):
                    var negativeBalanceNotificationsRules = (from notificationRule in _context.Set<NotificationRule>() where notificationRule.UserId == userId && notificationRule.RuleType == NotificationRuleType.NEGATIVE_BALANCE select notificationRule).ToList();
                    return negativeBalanceNotificationsRules;
                default:
                    throw new Exception($"Notificatiom rule type {ruleType} is not found");
            };
        }

    }
}