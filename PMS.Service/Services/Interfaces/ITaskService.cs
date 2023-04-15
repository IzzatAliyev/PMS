using PMS.Service.ViewModels.PTask;

namespace PMS.Service.Services.Interfaces
{
    public interface ITaskService
    {
        Task CreateTask(PTaskViewModel task);
        Task UpdateTaskById(int id, PTaskViewModel task);
        Task DeleteTaskById(int id);
        Task<PTaskViewModel> GetTaskById(int id);
        IEnumerable<PTaskViewModel> GetAllTasks();
    }
}