using PMS.Service.ViewModels.Employee;

namespace PMS.Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateEmployee(EmployeeViewModel employee);
        Task UpdateEmployeeById(int id, EmployeeViewModel employee);
        Task DeleteEmployeeById(int id);
        Task<EmployeeViewModel> GetEmployeeById(int id);
        IEnumerable<EmployeeViewModel> GetAllEmployees();
    }
}