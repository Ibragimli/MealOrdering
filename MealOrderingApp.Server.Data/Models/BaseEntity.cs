using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Server.Data.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
