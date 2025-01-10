using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Shared.DTOs
{
    public class OrderItemsDTO
    {
        public int Id { get; set; }


        public string CreatedUserId { get; set; }

        public int OrderId { get; set; }
        public DateTime CreateDate { get; set; }

        public string Description { get; set; }

        public string CreatedUserFullName { get; set; }

        public string OrderName { get; set; }
    }
}
