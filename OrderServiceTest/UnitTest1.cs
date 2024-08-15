using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OrderService.Controllers;
using OrderService.Models;
using OrderService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderServiceTest
{
    public class UnitTest1
    {
        private OrdersController _controller;
        private Mock<IOrderService> _mockOrderService;

        [SetUp]
        public void Initialize()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrdersController(_mockOrderService.Object, null); // Passing null for the logger
        }

        // Test method for GetAllOrders method in OrdersController
        [Test]
        public async Task TestGetAllOrdersPositive()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { OrderId = 1, Year = 2020, Make = "Toyota", Model = "Camry", DamageType = "Minor" },
                new Order { OrderId = 2, Year = 2021, Make = "Honda", Model = "Accord", DamageType = "Major" }
            };

            _mockOrderService.Setup(service => service.GetAllOrders()).ReturnsAsync(orders);

            // Act
            var result = await _controller.GetAllOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, (result.Result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf<List<Order>>((result.Result as OkObjectResult).Value);
            var returnedOrders = (result.Result as OkObjectResult).Value as List<Order>;
            Assert.AreEqual(2, returnedOrders.Count);
        }

        // Negative test method for GetAllOrders method in OrdersController
        [Test]
        public async Task TestGetAllOrdersNegative()
        {
            // Arrange
            _mockOrderService.Setup(service => service.GetAllOrders()).ReturnsAsync((List<Order>)null);

            // Act
            var result = await _controller.GetAllOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
