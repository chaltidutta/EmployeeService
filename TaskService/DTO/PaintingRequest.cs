
namespace TaskService.DTO
{
    //Represents a DTO(Data Transfer Object) for painting requests.
    public class PaintingRequest
    {
        public string? color { get; set; }
        // Gets or sets the color of the paint.
        public string? type { get; set; }
        // Gets or sets the type of paint (e.g., powder, urethane).
        public string? note { get; set; }
        // Gets or sets additional notes added by painter
    }
}
