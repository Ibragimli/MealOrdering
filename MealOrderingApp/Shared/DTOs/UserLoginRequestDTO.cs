using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Shared.DTOs
{
    public class UserLoginRequestDTO
    {
        public string UsernameOrEmail { get; set; }

        public string Password { get; set; }

    }
}