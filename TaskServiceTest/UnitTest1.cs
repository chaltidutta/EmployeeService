using Azure.Core;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskService.Controllers;
using TaskService.DTO;
using TaskService.Models;
using TaskService.Services;
namespace TaskServiceTest
{
    public class UnitTest1
    {
        TasksController t;
        [SetUp]
        public void Initialize()
        {
        }

        //testing the PerformPainting method in the TasksController.
        [Test]
        public void TestRegisterPositive()
        {
            // Creating a PaintingRequest object 'c' and initializing its properties
            PaintingRequest c = new PaintingRequest() { color = "Green", type = "Matt", note = "I am doing paint"};
           
            var mock = new Mock<ITaskService>();
           
            t = new TasksController(mock.Object,null);// Creating an instance of TasksController and injecting the mock object and null as dependencies
            
            mock.Setup(service => service.Painting(It.IsAny<int>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()))
            .ReturnsAsync("Painted");// ReturnsAsync method is configured to return "Painted"
           
            Assert.IsAssignableFrom<Task<ActionResult>>(t.PerformPainting(2,c));
            // Asserting that the result of PerformPainting method is of type Task<ActionResult>
        }

        [Test]
        public void TestValidateNegative()
        {
            Assert.Pass();
        }

    }
}