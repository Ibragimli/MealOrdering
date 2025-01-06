using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Server.Data.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }


        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<OrderItem> CreatedOrderItems { get; set; }
    }
}
