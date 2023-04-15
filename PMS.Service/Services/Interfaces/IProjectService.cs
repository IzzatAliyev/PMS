using PMS.Service.ViewModels.EmployeeProject;
using PMS.Service.ViewModels.Project;
using PMS.Service.ViewModels.PTask;

namespace PMS.Service.Services.Interfaces
{
    public interface IProjectService
    {
        Task CreateProject(ProjectViewModel employee);
        Task UpdateProjectById(int id, ProjectViewModel employee);
        Task DeleteProjectById(int id);
        Task<ProjectViewModel> GetProjectById(int id);
        IEnumerable<ProjectViewModel> GetAllProjects();
        IEnumerable<EmployeeProjectSearchViewModel> GetProjectsBySearchInput(string input);
        IEnumerable<PTaskViewModel> GetTasksByProjectId(int projectId);
    }
}