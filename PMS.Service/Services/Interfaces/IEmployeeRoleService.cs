using PMS.Service.ViewModels.EmployeeRole;

namespace PMS.Service.Services.Interfaces
{
    public interface IEmployeeRoleService
    {
        Task CreateEmployeeRole(EmployeeRoleViewModel employeeRole);
        Task UpdateEmployeeRoleById(int id, EmployeeRoleViewModel employeeRole);
        Task DeleteEmployeeRoleById(int id);
        Task<EmployeeRoleViewModel> GetEmployeeRoleById(int id);
        IEnumerable<EmployeeRoleViewModel> GetAllEmployeeRoles();
    }
}