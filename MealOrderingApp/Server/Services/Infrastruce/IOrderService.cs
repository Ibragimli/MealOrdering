using MealOrderingApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MealOrderingApp.Server.Services.Infrastruce
{
    public interface IOrderService
    {
        public Task<OrderDTO> CreateOrder(OrderDTO Order);

        public Task<OrderDTO> UpdateOrder(OrderDTO Order);

        public Task DeleteOrder(int OrderId);

        public Task<List<OrderDTO>> GetOrders(DateTime OrderDate);

        //public Task<List<OrderDTO>> GetOrdersByFilter(OrderListFilterModel Filter);

        public Task<OrderDTO> GetOrderById(int Id);



        public Task<OrderItemsDTO> CreateOrderItem(OrderItemsDTO OrderItem);

        public Task<OrderItemsDTO> UpdateOrderItem(OrderItemsDTO OrderItem);

        public Task<List<OrderItemsDTO>> GetOrderItems(int OrderId);

        public Task<OrderItemsDTO> GetOrderItemsById(int Id);

        public Task DeleteOrderItem(int OrderItemId);
    }
}
