using PMS.Service.ViewModels.EmployeeProject;

namespace PMS.Service.Services.Interfaces
{
    public interface IEmployeeProjectService
    {
        Task CreateEmployeeProject(EmployeeProjectViewModel employeeProject);
        Task UpdateEmployeeProjectById(int id, EmployeeProjectViewModel employeeProject);
        Task DeleteEmployeeProjectById(int id);
        Task<EmployeeProjectViewModel> GetEmployeeProjectById(int id);
        IEnumerable<EmployeeProjectViewModel> GetAllEmployeeProjects();
    }
}