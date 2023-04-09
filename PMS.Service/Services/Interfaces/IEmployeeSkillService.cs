using PMS.Service.ViewModels.EmployeeSkill;

namespace PMS.Service.Services.Interfaces
{
    public interface IEmployeeSkillService
    {
        Task CreateEmployeeSkill(EmployeeSkillViewModel employeeSkill);
        Task UpdateEmployeeSkillById(int id, EmployeeSkillViewModel employeeSkill);
        Task DeleteEmployeeSkillById(int id);
        Task<EmployeeSkillViewModel> GetEmployeeSkillById(int id);
        IEnumerable<EmployeeSkillViewModel> GetAllEmployeeSkills();
    }
}