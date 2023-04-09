using PMS.Service.ViewModels.ProjectRole;

namespace PMS.Service.Services.Interfaces
{
    public interface IProjectRoleService
    {
        Task CreateProjectRole(ProjectRoleViewModel projectRole);
        Task UpdateProjectRoleById(int id, ProjectRoleViewModel projectRole);
        Task DeleteProjectRoleById(int id);
        Task<ProjectRoleViewModel> GetProjectRoleById(int id);
        IEnumerable<ProjectRoleViewModel> GetAllProjectRoles();
    }
}