using MealOrderingApp.Server.Data.Models;

namespace MealOrderingApp.Server.Services.Infrastruce
{
    public interface ITokenHandler
    {
        public string GenerateJwtToken(User user);

    }
}
