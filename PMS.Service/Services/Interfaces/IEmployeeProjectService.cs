using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeProject;
using PMS.Service.ViewModels.Project;

namespace PMS.Service.Services.Interfaces
{
    public interface IEmployeeProjectService
    {
        Task CreateEmployeeProject(EmployeeProjectViewModel employeeProject);
        Task UpdateEmployeeProjectById(int id, EmployeeProjectViewModel employeeProject);
        Task DeleteEmployeeProjectById(int id);
        Task DeleteDuplicateEmployeeProjects();
        Task<EmployeeProjectViewModel> GetEmployeeProjectById(int id);
        IEnumerable<EmployeeProjectViewModel> GetAllEmployeeProjects();
        IEnumerable<ProjectViewModel> GetProjectsByEmployeeId(int employeeId);
        IEnumerable<EmployeeWithRoleViewModel> GetEmployeesByProjectId(int projectId);
    }
}