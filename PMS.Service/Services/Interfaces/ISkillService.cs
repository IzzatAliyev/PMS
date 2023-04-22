using PMS.Service.ViewModels.Skill;

namespace PMS.Service.Services.Interfaces
{
    public interface ISkillService
    {
        Task CreateSkill(SkillViewModel employee);
        Task UpdateSkillById(int id, SkillViewModel employee);
        Task DeleteSkillById(int id);
        Task<SkillViewModel> GetSkillById(int id);
        SkillViewModel GetSkillByName(string name);
        IEnumerable<SkillViewModel> GetAllSkills();
    }
}