using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Models;
using OrderService.DTOs;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int orderId);
        Task<bool> AddOrder(OrderRequest orderRequest, string createdBy);
        Task<bool> UpdateOrder(int orderId, OrderRequest orderRequest, string updatedBy);
        Task<bool> DeleteOrder(int orderId, string deletedBy);
    }
}
