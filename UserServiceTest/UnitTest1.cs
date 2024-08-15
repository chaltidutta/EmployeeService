using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using UserService.Controllers;
using UserService.Models;
using UserService.ServiceLayer;

namespace UserServiceTest
{
    public class UserControllerTest
    {
        private UserController _controller;
        private Mock<IUserService> _mockUserService;

        [SetUp]
        public void Initialize()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object, null); // Passing null for the logger
        }

        // Positive test method for GetAllUsers method in UserController
        [Test]
        public void TestGetAllUsersPositive()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserId = 1, Username = "manager1", PasswordHash = "password123", Role = "Manager" },
                new User { UserId = 2, Username = "inventoryWorker1", PasswordHash = "password123", Role = "InventoryWorker" }
            };

            _mockUserService.Setup(service => service.GetAllUsers()).Returns(users);

            // Act
            var result = _controller.Get() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<List<User>>(result.Value);
            var returnedUsers = result.Value as List<User>;
            Assert.AreEqual(2, returnedUsers.Count);
        }

        // Negative test method for GetAllUsers method in UserController
        [Test]
        public void TestGetAllUsersNegative()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetAllUsers()).Returns((List<User>)null);

            // Act
            var result = _controller.Get() as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
