using System;

namespace MealOrderingApp.Server.Services.Infrastruce
{
    public interface IValidationService
    {
        public bool IsAdmin(string UserId);

        public bool HasPermissionToChange(string UserId);

        public bool HasPermission(string UserId);
    }
}
