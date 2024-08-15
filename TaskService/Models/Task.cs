using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskService.Models
{
    [Table("NewTask1")]
    // Specifies that instances of this class will be stored in a table named "NewTask1" in the database.
    public class Task
    {
        [Key]
        // Specifies that this property (Id) is the primary key for the table.
        public int? Id { get; set; }

        public int? OrderId { get; set; }
        // Identifies the order associated with this task to properly store data in the database.

        public string? PackagingNote { get; set; }
        // Note added by the packager while performing the task.

        public int? InspectionRating { get; set; }
        // Final inspection rating, ranging from 1 (redo) to 5 (perfect).

        public string? SandblastingandSolderingNote { get; set; }
        // Note added by the solderer during sandblasting and soldering.

        public string? SandblastingandSolderingAmount { get; set; }
        // Level of sandblasting required for the wheel, e.g., low, medium, high.

        public string? PaintingNote { get; set; }
        // Note added by the painter during painting.

        public string? stepname { get; set; }
        // Current step being completed in the task.

        public string? color { get; set; }
        // Color to be used in painting the wheel, e.g., chrome, gray, black, pearl white.

        public string? status { get; set; }
        // Last operation completed in the task, e.g., Soldering, Painting, Packaging.

        public string? PaintType { get; set; }
        // Type of paint used in the wheel, e.g., powder, urethane.

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        // Timestamp indicating when the task was created, set to UTC time.

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        // Timestamp indicating when the task was last updated, using local time.

        public string? ImagePath { get; set; }
        // Path to the current image of the wheel uploaded by the packager.
    }
}
