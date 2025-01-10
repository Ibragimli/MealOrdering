using MealOrderingApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MealOrderingApp.Server.Services.Infrastruce
{
    public interface ISupplierService
    {
        public Task<List<SupplierDTO>> GetSuppliers();

        public Task<SupplierDTO> CreateSupplier(SupplierDTO supplierDTO);

        public Task<SupplierDTO> UpdateSupplier(SupplierDTO supplierDTO);

        public Task DeleteSupplier(int SupplierId);

        public Task<SupplierDTO> GetSupplierById(int Id);
    }
}
