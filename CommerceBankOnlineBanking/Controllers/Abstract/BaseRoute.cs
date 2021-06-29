using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommerceBankOnlineBanking.Services.Abstract;
namespace CommerceBankOnlineBanking.Controllers.Abstract
{
    public abstract class BaseRoute : Controller
    {
        private IAuthenticationService _authenticationService;

        public BaseRoute(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public int Authenticate()
        {
            return _authenticationService.Authenticate();
        }
        public String GetUserId()
        {
            return _authenticationService.GetUserId();
        }
        public void CreateAuthUserId(string userId)
        {
            _authenticationService.CreateAuthUserId(userId);
        }

    }
}
