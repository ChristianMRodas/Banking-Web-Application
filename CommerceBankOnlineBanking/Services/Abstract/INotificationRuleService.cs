using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommerceBankOnlineBanking.Services.Abstract
{
    public interface INotificationRuleService
    {
        bool EnableNotificationRule(Guid userId, string notificationRule);
        bool DisableNotificationRule(Guid userId, string notificationRule);
        bool GetNotificationRuleEnabled(Guid userId, string notificationRule);

    }
}