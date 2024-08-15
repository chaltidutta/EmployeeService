using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskService.Models;
using TaskService.Services;
using Microsoft.AspNetCore.Authorization;
using TaskService.DTO;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.AspNetCore.Cors;

namespace TaskService.Controllers
{
    [EnableCors("MyPolicy")] //Enabling CORS
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        ILogger _logger;//logging 

        [HttpGet("GetRoleAndUsername")]
        [Authorize]
        public IEnumerable<string> Get()
        {
            var token = this.User;
            return new string[] {
                token.FindFirst("Role").Value.ToString(),//extract the values stored in payload of the token
                token.FindFirst("Username").Value.ToString() };
        }

        private readonly ITaskService _service;
        public TasksController(ITaskService service, ILogger<TasksController> log)
        {
            _logger = log; // Assigns the injected ITaskService instance to _service field.
            _service = service;// Assigns the injected ILogger<TasksController> instance to _logger field.
        }

        [HttpPost("SolderingSandblasting")]
        //[Authorize(Roles = "Solderer")]// Specifies that access to the action method is restricted to users who have the 'Solderer' role.
        public ActionResult PerformSolderingAndSandblasting(int orderId, [FromBody] SolderingandBlastingRequest f)
        {
            _service.SolderingandSandblasting(orderId, f.level, f.note);
            _logger.LogInformation("Get called on :" + DateTime.Now.ToString() + Request.Host.Value);
            return Ok();
        }

        [HttpPost("Painting")]
        //        [Authorize(Roles ="Painter")]
        public async Task<ActionResult> PerformPainting(int orderId, [FromBody] PaintingRequest p)
        {
            _logger.LogInformation("Perform painting on :" + DateTime.Now.ToString() + Request.Host.Value);//logging

            try
            {
                var r = await _service.Painting(orderId, p.color, p.type, p.note);
                _logger.LogInformation("Get called on :" + DateTime.Now.ToString() + Request.Host.Value);
                return Ok(r);
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation("Get called on :" + DateTime.Now.ToString() + Request.Host.Value);
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("Packaging")]
        [Authorize(Roles = "PackagingWorker")]
        public async Task<ActionResult> PerformPackaging(int orderId, [FromBody] PackagingRequest q)
        {
            _logger.LogInformation("Perform packaging on :" + DateTime.Now.ToString() + Request.Host.Value);//logging

            try
            {
                var r = await _service.Packaging(orderId, q.inspectionRating, q.note, q.ImagePath);
                return Ok(r);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllStatus")]
        //[Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<Models.Task>>> GetAllTasks()
        {
            var tasks = await _service.GetAllTasks();
            _logger.LogInformation("Get called on :" + DateTime.Now.ToString() + Request.Host.Value);
            return Ok(tasks);
        }
        [HttpGet("SandblastAndSolderStatus/{orderId}")]
        [Authorize(Roles = "Manager,Solderer")]
        public async Task<ActionResult<List<string>>> GetSandblastAndSolderStatus(int orderId)
        {
            var statuss = await _service.SandblastSolderstatusByOrderId(orderId);

            if (statuss == null || statuss.Count() == 0)
            {
                return NotFound();
            }

            return Ok(statuss);
        }

        [HttpGet("PaintingStatus/{orderId}")]
        [Authorize(Roles = "Manager,Painter")]
        public async Task<ActionResult<List<string>>> GetPaintingstatusbyOrderId(int orderId)
        {
            var statuss = await _service.PaintingstatusbyOrderId(orderId);

            if (statuss == null || statuss.Count() == 0)
            {
                return NotFound();
            }

            return Ok(statuss);
        }

        [HttpGet("PackagingStatus/{orderId}")]
        [Authorize(Roles = "Manager,PackagingWorker")]
        public async Task<ActionResult<List<string>>> GetPackagingstatusbyOrderId(int orderId)
        {
            var statuss = await _service.PackagingstatusbyOrderId(orderId);

            if (statuss == null || statuss.Count() == 0)
            {
                return NotFound();
            }

            return Ok(statuss);
        }

        [HttpGet("OrderStatus/{orderId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<string>> GetOrderCurrentStatus(int orderId)
        {
            var status = await _service.GetOrderStatus(orderId);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }
        [HttpPost("DeleteTask/{orderId}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteTask(int orderId)
        {
            try
            {
                await _service.DeleteTask(orderId);
                return Ok("Task deleted successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
