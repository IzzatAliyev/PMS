using PMS.Core.Repositories;
using PMS.Infrastructure.Data;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Skill;

namespace PMS.Service.Services.Impl
{
    public class SkillService : ISkillService
    {
        private readonly PMSDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public SkillService(PMSDbContext context, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateSkill(SkillViewModel skill)
        {
            var newSkill = new Skill()
            {
                Id = skill.Id,
                Name = skill.Name != null ? skill.Name : string.Empty,
                Description = skill.Description != null ? skill.Description : string.Empty,
                ColorCode = skill.ColorCode != null ? skill.ColorCode : string.Empty
            };
            await this.unitOfWork.GenericRepository<Skill>().AddAsync(newSkill);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateSkillById(int id, SkillViewModel skill)
        {
            var skillOld = await this.unitOfWork.GenericRepository<Skill>().GetByIdAsync(id);
            if (skillOld != null)
            {
                skillOld.Name = skill.Name != null ? skill.Name : skillOld.Name;
                skillOld.Description = skill.Description != null ? skill.Description : skillOld.Description;
                skillOld.ColorCode = skill.ColorCode != null ? skill.ColorCode : skillOld.ColorCode;
                skillOld.UpdatedAt = DateTime.UtcNow;
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

        public SkillViewModel GetSkillByName(string name)
        {
            var skillDb = this.context.Skills.Where(e => e.Name == name).First();
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
                throw new Exception("name doesn't exist");
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