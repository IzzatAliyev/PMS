using PMS.Service.ViewModels.ProjectTask;

namespace PMS.Service.Services.Interfaces
{
    public interface IProjectTaskService
    {
        Task CreateTask(ProjectTaskViewModel task);
        Task UpdateTaskById(int id, ProjectTaskViewModel task);
        Task DeleteTaskById(int id);
        Task<ProjectTaskViewModel> GetTaskById(int id);
        IEnumerable<ProjectTaskViewModel> GetAllTasks();
    }
}