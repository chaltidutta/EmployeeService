namespace TaskService.DTO
{
    public class PackagingRequest
    {
        public int inspectionRating { get; set; }
        public string note { get; set; }

        public string? ImagePath { get; set; }
    }
}