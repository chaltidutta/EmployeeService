using Microsoft.EntityFrameworkCore;

namespace TaskService.Services
{
    public interface ITaskService
    {
        Task<string> Painting(int orderId, string color, string paintType, string notes);
        Task<string> SolderingandSandblasting(int OrderId, string SandblastingandSolderingAmount, string notes);

         Task<string> Packaging(int orderId, int inspectionRating, string notes, string ImagePath);

        Task<List<Models.Task>> GetAllTasks();

        Task<string> SandblastSolderstatusByOrderId(int orderId);
        Task<string> PaintingstatusbyOrderId(int orderId);
        Task<string> PackagingstatusbyOrderId(int orderId);
        Task<string> GetOrderStatus(int orderId);
        Task<string> DeleteTask(int orderId);





    }
}
