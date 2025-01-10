using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Shared.DTOs
{
    public class UserLoginResponseDTO
    {
        public string Token { get; set; }
        public UserDTO User { get; set; }

    }
}