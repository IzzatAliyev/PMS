using PMS.Service.ViewModels.Project;

namespace PMS.Service.Services.Interfaces
{
    public interface IProjectService
    {
        Task CreateProject(ProjectViewModel employee);
        Task UpdateProjectById(int id, ProjectViewModel employee);
        Task DeleteProjectById(int id);
        Task<ProjectViewModel> GetProjectById(int id);
        IEnumerable<ProjectViewModel> GetAllProjects();
    }
}