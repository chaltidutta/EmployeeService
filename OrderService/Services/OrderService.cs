using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using OrderService.DTOs;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _context;

        public OrderService(OrderContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }

        public async Task<bool> AddOrder(OrderRequest orderRequest, string createdBy)
        {
            var newOrder = new Order
            {
                Year = orderRequest.Year,
                Make = orderRequest.Make,
                Model = orderRequest.Model,
                DamageType = orderRequest.DamageType,
                Notes = orderRequest.Notes,
                ImagePath = orderRequest.ImagePath,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrder(int orderId, OrderRequest orderRequest, string updatedBy)
        {
            var existingOrder = await _context.Orders.FindAsync(orderId);

            if (existingOrder == null) return false;

            existingOrder.Year = orderRequest.Year;
            existingOrder.Make = orderRequest.Make;
            existingOrder.Model = orderRequest.Model;
            existingOrder.DamageType = orderRequest.DamageType;
            existingOrder.Notes = orderRequest.Notes;
            existingOrder.ImagePath = orderRequest.ImagePath;
            existingOrder.UpdatedBy = updatedBy;
            existingOrder.UpdatedAt = DateTime.UtcNow;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(int orderId, string deletedBy)
        {
            var existingOrder = await _context.Orders.FindAsync(orderId);

            if (existingOrder == null) return false;

            existingOrder.DeletedBy = deletedBy;
            existingOrder.DeletedAt = DateTime.UtcNow;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
