using TaskService.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace TaskService.Services
{
    public class TaskServices : ITaskService
    {
        private readonly TaskContext _context;
        public TaskServices(TaskContext context)
        {
            _context = context;
        }
        public async Task<string> SolderingandSandblasting(int OrderId, string SandblastingandSolderingAmount, string notes)
        {
            // Check if a task with the given OrderId already exists in the database
            // var existingTask;
            var existingTask = await _context.tasks.FindAsync( OrderId);
            
            if (existingTask != null)
            {
                // Update existing task
                existingTask.SandblastingandSolderingAmount = SandblastingandSolderingAmount;
                existingTask.stepname = "Sandblasting and soldering";
                existingTask.status = "Soldered and Sandblasted";
                existingTask.SandblastingandSolderingNote = $"Sandblasting: {SandblastingandSolderingAmount}. Notes: {notes}";
                existingTask.CreatedAt = DateTime.UtcNow;
                _context.tasks.Update(existingTask);
            }
            else
            {
                // Create new task
                Models.Task newTask = new Models.Task
                {
                    OrderId = OrderId,
                    SandblastingandSolderingAmount = SandblastingandSolderingAmount,
                    stepname = "Sandblasting and soldering",
                    status = "Soldered and Sandblasted",
                    SandblastingandSolderingNote = $"Sandblasting: {SandblastingandSolderingAmount}. Notes: {notes}",
                    CreatedAt = DateTime.UtcNow
                };
                existingTask = newTask;
                await _context.tasks.AddAsync(newTask);
            }

            await _context.SaveChangesAsync();

            return existingTask.status;
        }
        public async Task<string> Painting(int orderId, string color, string paintType, string notes)
        {
            // Check if a task with the given OrderId already exists in the database
            var existingTask = await _context.tasks.FindAsync(orderId);
            if (existingTask != null && existingTask.status == "Soldered and Sandblasted")
            {
                // Update existing task
                existingTask.PaintType = paintType;
                existingTask.stepname = "Painting";
                existingTask.status = "Painted";
                existingTask.color = color;
                existingTask.PaintingNote += $"Color: {color}, Paint Type: {paintType}, Notes: {notes}";
                existingTask.UpdatedAt = DateTime.UtcNow;
                _context.tasks.Update(existingTask);
            }
            else throw new ArgumentException("Task not found or not yet sandblasted and soldered.");

            await _context.SaveChangesAsync();
            return existingTask.status;
        }

        public async Task<string> Packaging(int orderId, int inspectionRating, string notes, string ImagePath)
        {
            // Check if a task with the given OrderId already exists in the database
            var existingTask = await _context.tasks.FindAsync(orderId);

            // Update existing task
            if (existingTask != null && existingTask.status == "Painted")
            {
                existingTask.InspectionRating = inspectionRating;
                existingTask.stepname = "Packaging";
                existingTask.status = "Packaged";
                existingTask.PackagingNote += $"Inspection Rating: {inspectionRating}. Notes: {notes}";
                existingTask.UpdatedAt = DateTime.UtcNow; // Optionally update UpdatedAt timestamp
                existingTask.ImagePath = ImagePath;
                _context.tasks.Update(existingTask);
            }
            else throw new ArgumentException("Task not found or not yet painted.");

            await _context.SaveChangesAsync();
            return existingTask.status;
        }


        public async Task<List<Models.Task>> GetAllTasks()
        {
            return await _context.tasks.ToListAsync();
        }
        public async Task<string> SandblastSolderstatusByOrderId(int orderId)
        {
            var task = await _context.tasks
                .Where(t => t.OrderId == orderId)
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return $"Not yet soldered or sandblasted";
            }
            else if (task.status == "Painted" || task.status == "Packaged")
                return $"Soldered and Sandblasted";
            return task.status;
        }
        public async Task<string> PaintingstatusbyOrderId(int orderId)
        {
            var task = await _context.tasks
                .Where(t => t.OrderId == orderId)
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return $"No such order";
            }
            else if (task.status == "Soldered and Sandblasted")
            {
                return $"Not yet painted";
            }
            else if (task.status == "Packaged")
                return $"Painted";

            return task.status;
        }

        public async Task<string> PackagingstatusbyOrderId(int orderId)
        {
            var task = await _context.tasks
                .Where(t => t.OrderId == orderId)
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return $"No such order";
            }
            else if (task.status == "Painted")
            {
                return $"Not yet packaged";
            }

            return task.status;
        }

        public async Task<string> GetOrderStatus(int orderId)
        {
            var task = await _context.tasks
                .Where(t => t.OrderId == orderId)
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return $"Order Not Found"; // or throw an exception if needed
            }

            return task.status; // Assuming Status is the property you want to return
        }
        public async Task<string> DeleteTask(int orderId)
        {
            // Find the task with the given OrderId
            var taskToDelete = await _context.tasks.FindAsync(orderId);

            if (taskToDelete != null)
            {
                _context.tasks.Remove(taskToDelete); // Mark task for deletion
                await _context.SaveChangesAsync(); // Save deletion to the database
                return $"Deleted row";
            }
            else
            {
                throw new ArgumentException("Task not found for deletion");
            }
        }

    }
}
