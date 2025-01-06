using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Server.Data.Models
{
    public class OrderItem : BaseEntity
    {
        public string CreateUserId { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}
