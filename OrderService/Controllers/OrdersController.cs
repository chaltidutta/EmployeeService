using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrderService.Models;
using OrderService.Services;
using OrderService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace OrderService.Controllers
{
    [EnableCors("MyPolicy")] //Enabling CORS
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        ILogger _logger; //logging

        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> log)
        {
            _orderService = orderService;
            _logger = log;
        }

        [HttpGet]
        //[Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        //[Authorize(Roles = "InventoryWorker")]
        public async Task<ActionResult> AddOrder([FromBody] OrderRequest orderRequest)
        {
            _logger.LogInformation("Ordered added at :" + DateTime.Now.ToString() + Request.Host.Value); //logging

            var createdBy = User.FindFirst("Username")?.Value;
            await _orderService.AddOrder(orderRequest, createdBy);
            return Ok();
        }

        [HttpPut("{orderId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> UpdateOrder(int orderId, [FromBody] OrderRequest orderRequest)
        {
            _logger.LogInformation("order updated at :" + DateTime.Now.ToString() + Request.Host.Value); //logging

            var updatedBy = User.FindFirst("Username")?.Value;
            var result = await _orderService.UpdateOrder(orderId, orderRequest, updatedBy);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var deletedBy = User.FindFirst("Username")?.Value;
            var result = await _orderService.DeleteOrder(orderId, deletedBy);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
