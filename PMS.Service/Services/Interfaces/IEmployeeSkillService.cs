using PMS.Service.ViewModels.EmployeeSkill;
using PMS.Service.ViewModels.Skill;

namespace PMS.Service.Services.Interfaces
{
    public interface IEmployeeSkillService
    {
        Task CreateEmployeeSkill(EmployeeSkillViewModel employeeSkill);
        Task UpdateEmployeeSkillById(int id, EmployeeSkillViewModel employeeSkill);
        Task DeleteEmployeeSkillById(int id);
        Task<EmployeeSkillViewModel> GetEmployeeSkillById(int id);
        IEnumerable<EmployeeSkillViewModel> GetAllEmployeeSkills();
        IEnumerable<SkillViewModel> GetSkillsByEmployeeId(int employeeId);
    }
}