using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Server.Data.Models
{
    public class Order : BaseEntity
    {
        public string CreateUserId { get; set; }
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExpiredDate { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual User User { get; set; }
        public virtual Supplier Supplier { get; set; }

    }
}
