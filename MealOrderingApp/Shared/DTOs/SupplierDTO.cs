using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrderingApp.Shared.DTOs
{
    public class SupplierDTO
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string WebURL { get; set; }
        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }
    }
}
