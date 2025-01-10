using AutoMapper;
using MealOrderingApp.Server.Services.Infrastruce;
using MealOrderingApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MealOrderingApp.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper.QueryableExtensions;
using MealOrderingApp.Server.Data.Models;

namespace MealOrderingApp.Server.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IValidationService validationService;

        public OrderService(DataContext Context, IMapper Mapper, IValidationService ValidationService)
        {
            _context = Context;
            _mapper = Mapper;
            validationService = ValidationService;
        }


        #region Order Methods


        #region Get

        //public async Task<List<OrderDTO>> GetOrdersByFilter(OrderListFilterModel Filter)
        //{
        //    var query = _context.Orders.Include(i => i.Supplier).AsQueryable();

        //    if (Filter.CreatedUserId != Guid.Empty)
        //        query = query.Where(i => i.CreatedUserId == Filter.CreatedUserId);

        //    if (Filter.CreateDateFirst.HasValue)
        //        query = query.Where(i => i.CreateDate >= Filter.CreateDateFirst);

        //    if (Filter.CreateDateLast > DateTime.MinValue)
        //        query = query.Where(i => i.CreateDate <= Filter.CreateDateLast);


        //    var list = await query
        //              .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
        //              .OrderBy(i => i.CreateDate)
        //              .ToListAsync();

        //    return list;
        //}


        public async Task<List<OrderDTO>> GetOrders(DateTime OrderDate)
        {
            var list = await _context.Orders.Include(i => i.Supplier)
                      .Where(i => i.CreatedTime.Date == OrderDate.Date)
                      .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();

            return list;
        }



        public async Task<OrderDTO> GetOrderById(int Id)
        {
            return await _context.Orders.Where(i => i.Id == Id)
                      .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        #endregion

        #region Post

        public async Task<OrderDTO> CreateOrder(OrderDTO order)
        {
            var dbOrder = _mapper.Map<Order>(order);
            await _context.AddAsync(dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(dbOrder);
        }

        public async Task<OrderDTO> UpdateOrder(OrderDTO order)
        {
            var dbOrder = await _context.Orders.FirstOrDefaultAsync(i => i.Id == order.Id);
            if (dbOrder == null)
                throw new Exception("Order not found");


            if (!validationService.HasPermission(dbOrder.CreateUserId))
                throw new Exception("You cannot change the order unless you created");

            _mapper.Map(order, dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(dbOrder);
        }

        public async Task DeleteOrder(int orderId)
        {
            var detailCount = await _context.OrderItems.Where(i => i.OrderId == orderId).CountAsync();


            if (detailCount > 0)
                throw new Exception($"There are {detailCount} sub items for the order you are trying to delete");

            var order = await _context.Orders.FirstOrDefaultAsync(i => i.Id == orderId);
            if (order == null)
                throw new Exception("Order not found");


            if (!validationService.HasPermission(order.CreateUserId))
                throw new Exception("You cannot change the order unless you created");

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }

        #endregion

        #endregion


        #region OrderItem Methods

        #region Get

        public async Task<List<OrderItemsDTO>> GetOrderItems(int orderId)
        {
            return await _context.OrderItems.Where(i => i.OrderId == orderId)
                      .ProjectTo<OrderItemsDTO>(_mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();
        }

        public async Task<OrderItemsDTO> GetOrderItemsById(int id)
        {
            return await _context.OrderItems.Include(i => i.Order).Where(i => i.Id == id)
                      .ProjectTo<OrderItemsDTO>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        #endregion

        #region Post


        public async Task<OrderItemsDTO> CreateOrderItem(OrderItemsDTO orderItem)
        {
            var order = await _context.Orders
                .Where(i => i.Id == orderItem.OrderId)
                .Select(i => i.ExpiredDate)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception("The main order not found");

            if (order <= DateTime.Now)
                throw new Exception("You cannot create sub order. It is expired !!!");


            var dbOrder = _mapper.Map<OrderItem>(orderItem);
            await _context.AddAsync(dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemsDTO>(dbOrder);
        }

        public async Task<OrderItemsDTO> UpdateOrderItem(OrderItemsDTO orderItem)
        {
            var dbOrder = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == orderItem.Id);
            if (dbOrder == null)
                throw new Exception("Order not found");

            _mapper.Map(orderItem, dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemsDTO>(dbOrder);
        }

        public async Task DeleteOrderItem(int OrderItemId)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItemId);
            if (orderItem == null)
                throw new Exception("Sub order not found");

            _context.OrderItems.Remove(orderItem);

            await _context.SaveChangesAsync();
        }



        #endregion

        #endregion
    }
}
