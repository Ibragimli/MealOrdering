using AutoMapper;
using MealOrderingApp.Server.Services.Infrastruce;
using MealOrderingApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MealOrderingApp.Server.Data.Context;
using AutoMapper.QueryableExtensions;
using MealOrderingApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MealOrderingApp.Server.Services.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SupplierService(DataContext Context, IMapper Mapper)
        {

            _context = Context;
            _mapper = Mapper;
        }


        public async Task<List<SupplierDTO>> GetSuppliers()
        {
            var list = await _context.Suppliers//.Where(i => i.IsActive)
                      .ProjectTo<SupplierDTO>(_mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();

            return list;
        }

        #region Post

        public async Task<SupplierDTO> CreateSupplier(SupplierDTO Supplier)
        {
            var dbSupplier = _mapper.Map<Supplier>(Supplier);
            await _context.AddAsync(dbSupplier);
            await _context.SaveChangesAsync();

            return _mapper.Map<SupplierDTO>(dbSupplier);
        }

        public async Task<SupplierDTO> UpdateSupplier(SupplierDTO Supplier)
        {
            var dbSupplier = await _context.Suppliers.FirstOrDefaultAsync(i => i.Id == Supplier.Id);
            if (dbSupplier == null)
                throw new Exception("Supplier not found");

            _mapper.Map(Supplier, dbSupplier);
            await _context.SaveChangesAsync();

            return _mapper.Map<SupplierDTO>(dbSupplier);
        }

        public async Task DeleteSupplier(int SupplierId)
        {
            var Supplier = await _context.Suppliers.FirstOrDefaultAsync(i => i.Id == SupplierId);
            if (Supplier == null)
                throw new Exception("Supplier not found");

            int orderCount = await _context.Suppliers.Include(i => i.Orders).Select(i => i.Orders.Count).FirstOrDefaultAsync();

            if (orderCount > 0)
                throw new Exception($"There are {orderCount} sub order for the order you are trying to delete");

            _context.Suppliers.Remove(Supplier);
            await _context.SaveChangesAsync();
        }

        public async Task<SupplierDTO> GetSupplierById(int Id)
        {
            return await _context.Suppliers.Where(i => i.Id == Id)
                      .ProjectTo<SupplierDTO>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }


        #endregion
    }
}
