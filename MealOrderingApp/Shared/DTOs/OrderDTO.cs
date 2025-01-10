using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MealOrderingApp.Shared.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatedUserId { get; set; }
        public int SupplierId { get; set; }
        
        [MinLength(3, ErrorMessage = "Minimum lenght must be 3 characters for Name Field")]
        [StringLength(10)]
        public String Name { get; set; }

        [StringLength(100)]
        public String Description { get; set; }

        public DateTime ExpireDate { get; set; }

        public string CreatedUserFullName { get; set; }

        public string SupplierName { get; set; }
    }
}
