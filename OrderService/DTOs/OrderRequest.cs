namespace OrderService.DTOs
{
    public class OrderRequest
    {
        public int Year { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string DamageType { get; set; } = null!;
        public string? Notes { get; set; }
        public string? ImagePath { get; set; }
    }
}
