using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.EmployeeSkill;

namespace PMS.Service.Services.Impl
{
    public class EmployeeSkillService : IEmployeeSkillService
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeeSkillService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
                foreach(var employeeSkill in employeeSkillsDb)
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
    }
}