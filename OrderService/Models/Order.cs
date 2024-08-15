using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string DamageType { get; set; } = null!;
        public string? Notes { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
