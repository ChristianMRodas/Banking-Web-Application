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

namespace CommerceBankOnlineBanking.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private HttpContext _httpContext;

        private ILogger<AuthenticationService> _logger;
        public AuthenticationService(IHttpContextAccessor httpContext, ILogger<AuthenticationService> logger)
        {
            _httpContext = httpContext.HttpContext;
            _logger = logger;
        }
        public int Authenticate()
        {
            if (_httpContext.Request.Cookies.ContainsKey("AuthUserId"))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public String GetUserId()
        {
            _httpContext.Request.Cookies.TryGetValue("AuthUserId", out String userId);
            return userId;
        }

        public void CreateAuthUserId(string userId)
        {
            try
            {
                _httpContext.Response.Cookies.Append("AuthUserId", userId);
                _logger.LogInformation("Successfully created AuthUserId cookie for user {userId}", userId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create AuthUserId Cookie for user {userId}", userId);
            }
        }
    }
}