using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeProject;

namespace PMS.Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateEmployee(EmployeeViewModel employee);
        Task UpdateEmployeeById(int id, EmployeeViewModel employee);
        Task DeleteEmployeeById(int id);
        Task<EmployeeViewModel> GetEmployeeById(int id);
        EmployeeViewModel GetEmployeeByEmail(string email);
        EmployeeViewModel GetEmployeeByName(string name);
        IEnumerable<EmployeeViewModel> GetAllEmployees();
        IEnumerable<EmployeeProjectSearchViewModel> GetEmployeesBySearchInput(string input);
        Task SetProfilePictureByEmployeeId(int id, string url);
    }
}