using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Server.Data.Models
{
    public class Supplier:BaseEntity
    {
        public string Name { get; set; }
        public string WebUrl { get; set; }
        public virtual ICollection<Order> Orders { get; set;}
    }
}
