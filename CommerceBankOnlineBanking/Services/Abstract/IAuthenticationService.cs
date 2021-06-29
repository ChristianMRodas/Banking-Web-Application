using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommerceBankOnlineBanking.Services.Abstract
{
    public interface IAuthenticationService
    {
        int Authenticate();
        String GetUserId();

        void CreateAuthUserId(string userId);
    }
}