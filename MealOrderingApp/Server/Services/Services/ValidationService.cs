using MealOrderingApp.Server.Services.Infrastruce;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;

namespace MealOrderingApp.Server.Services.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ValidationService(IHttpContextAccessor HttpContextAccessor)
        {
            httpContextAccessor = HttpContextAccessor;
        }

        public bool HasPermission(string UserId)
        {
            return IsAdmin(UserId) || HasPermissionToChange(UserId);
        }

        public bool HasPermissionToChange(string UserId)
        {
            String userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData).Value;

            //return string(userId, out string result) ? result == UserId : false;
            return false;
        }

        public bool IsAdmin(string UserId)
        {
            return httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value == "salihcantekin1@gmail.com";
        }
    }
}
