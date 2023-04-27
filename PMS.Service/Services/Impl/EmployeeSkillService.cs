using Microsoft.EntityFrameworkCore;
using PMS.Core.Repositories;
using PMS.Infrastructure.Data;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.EmployeeSkill;
using PMS.Service.ViewModels.Skill;

namespace PMS.Service.Services.Impl
{
    public class EmployeeSkillService : IEmployeeSkillService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PMSDbContext dbcontext;

        public EmployeeSkillService(IUnitOfWork unitOfWork, PMSDbContext dbcontext)
        {
            this.unitOfWork = unitOfWork;
            this.dbcontext = dbcontext;
        }
        public async Task CreateEmployeeSkill(EmployeeSkillViewModel employeeSkill)
        {
            var newEmployeeSkill = new EmployeeSkill()
            {
                Id = employeeSkill.Id,
                EmployeeId = employeeSkill.EmployeeId,
                SkillId = employeeSkill.SkillId,
                Level = employeeSkill.Level
            };
            await this.unitOfWork.GenericRepository<EmployeeSkill>().AddAsync(newEmployeeSkill);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateEmployeeSkillById(int id, EmployeeSkillViewModel employeeSkill)
        {
            var employeeSkillOld = await this.unitOfWork.GenericRepository<EmployeeSkill>().GetByIdAsync(id);
            if (employeeSkillOld != null)
            {
                employeeSkillOld.EmployeeId = employeeSkill.EmployeeId;
                employeeSkillOld.SkillId = employeeSkill.SkillId;
                employeeSkillOld.Level = employeeSkill.Level;
                employeeSkillOld.UpdatedAt = DateTime.UtcNow;
                await this.unitOfWork.GenericRepository<EmployeeSkill>().UpdateAsync(employeeSkillOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteEmployeeSkillById(int id)
        {
            var employeeSkill = await this.unitOfWork.GenericRepository<EmployeeSkill>().GetByIdAsync(id);
            if (employeeSkill != null)
            {
                await this.unitOfWork.GenericRepository<EmployeeSkill>().DeleteAsync(employeeSkill);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteDuplicateEmployeeSkills()
        {
            var groupedRecords = await dbcontext.EmployeeSkills
            .GroupBy(es => new { es.EmployeeId, es.SkillId })
            .ToListAsync();

            var duplicateRecords = groupedRecords
                .Where(grp => grp.Count() > 1)
                .SelectMany(grp => grp.Skip(1));

            dbcontext.EmployeeSkills.RemoveRange(duplicateRecords);
            await dbcontext.SaveChangesAsync();
        }

        public async Task<EmployeeSkillViewModel> GetEmployeeSkillById(int id)
        {
            var employeeSkillDb = await this.unitOfWork.GenericRepository<EmployeeSkill>().GetByIdAsync(id);
            if (employeeSkillDb != null)
            {
                var employeeSkill = new EmployeeSkillViewModel()
                {
                    Id = employeeSkillDb.Id,
                    EmployeeId = employeeSkillDb.EmployeeId,
                    SkillId = employeeSkillDb.SkillId,
                    Level = employeeSkillDb.Level
                };
                return employeeSkill;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public IEnumerable<EmployeeSkillViewModel> GetAllEmployeeSkills()
        {
            var employeeSkills = new List<EmployeeSkillViewModel>();
            var employeeSkillsDb = this.unitOfWork.GenericRepository<EmployeeSkill>().GetAll();
            if (employeeSkillsDb != null)
            {
                foreach (var employeeSkill in employeeSkillsDb)
                {
                    var currentEmployeeSkill = new EmployeeSkillViewModel()
                    {
                        Id = employeeSkill.Id,
                        EmployeeId = employeeSkill.EmployeeId,
                        SkillId = employeeSkill.SkillId,
                        Level = employeeSkill.Level
                    };
                    employeeSkills.Add(currentEmployeeSkill);
                }
                return employeeSkills;
            }
            else
            {
                throw new Exception("employee skills doesn't exist");
            }
        }

        public IEnumerable<SkillViewModel> GetSkillsByEmployeeId(int employeeId)
        {
            var result = this.dbcontext.Employees
                .Include(e => e.Skills)
                .Where(e => e.Id == employeeId)
                .SelectMany(e => e.Skills.Select(s => new SkillViewModel
                {
                    Id = s.Skill.Id,
                    Name = s.Skill.Name,
                    Description = s.Skill.Description,
                    ColorCode = s.Skill.ColorCode
                }))
                .ToList();

            return result;
        }
    }
}