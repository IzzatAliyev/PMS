using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Skill;

namespace PMS.Service.Services.Impl
{
    public class SkillService : ISkillService
    {
        private readonly IUnitOfWork unitOfWork;

        public SkillService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateSkill(SkillViewModel skill)
        {
            var newSkill = new Skill()
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
                ColorCode = skill.ColorCode
            };
            await this.unitOfWork.GenericRepository<Skill>().AddAsync(newSkill);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateSkillById(int id, SkillViewModel skill)
        {
            var skillOld = await this.unitOfWork.GenericRepository<Skill>().GetByIdAsync(id);
            if (skillOld != null)
            {
                skillOld.Name = skill.Name;
                skillOld.Description = skill.Description;
                skillOld.ColorCode = skill.ColorCode;
                await this.unitOfWork.GenericRepository<Skill>().UpdateAsync(skillOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteSkillById(int id)
        {
            var skill = await this.unitOfWork.GenericRepository<Skill>().GetByIdAsync(id);
            if (skill != null)
            {
                await this.unitOfWork.GenericRepository<Skill>().DeleteAsync(skill);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task<SkillViewModel> GetSkillById(int id)
        {
            var skillDb = await this.unitOfWork.GenericRepository<Skill>().GetByIdAsync(id);
            if (skillDb != null)
            {
                var skill = new SkillViewModel()
                {
                    Id = skillDb.Id,
                    Name = skillDb.Name,
                    Description = skillDb.Description,
                    ColorCode = skillDb.ColorCode
                };
                return skill;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public IEnumerable<SkillViewModel> GetAllSkills()
        {
            var skills = new List<SkillViewModel>();
            var skillsDb = this.unitOfWork.GenericRepository<Skill>().GetAll();
            if (skillsDb != null)
            {
                foreach(var skill in skillsDb)
                {
                    var currentSkill = new SkillViewModel()
                    {
                        Id = skill.Id,
                        Name = skill.Name,
                        Description = skill.Description,
                        ColorCode = skill.ColorCode,
                    };
                    skills.Add(currentSkill);
                }
                return skills;
            }
            else
            {
                throw new Exception("skills doesn't exist");
            }
        }
    }
}