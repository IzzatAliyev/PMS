using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.EmployeeRole;

namespace PMS.Service.Services.Impl
{
    public class EmployeeRoleService : IEmployeeRoleService
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeeRoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateEmployeeRole(EmployeeRoleViewModel employeeRole)
        {
            var newEmployeeRole = new EmployeeRole()
            {
                Id = employeeRole.Id,
                EmployeeId = employeeRole.EmployeeId,
                Role = employeeRole.Role
            };
            await this.unitOfWork.GenericRepository<EmployeeRole>().AddAsync(newEmployeeRole);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateEmployeeRoleById(int id, EmployeeRoleViewModel employeeRole)
        {
            var employeeRoleOld = await this.unitOfWork.GenericRepository<EmployeeRole>().GetByIdAsync(id);
            if (employeeRoleOld != null)
            {
                employeeRoleOld.EmployeeId = employeeRole.EmployeeId;
                employeeRoleOld.Role = employeeRole.Role;
                await this.unitOfWork.GenericRepository<EmployeeRole>().UpdateAsync(employeeRoleOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteEmployeeRoleById(int id)
        {
            var employeeRole = await this.unitOfWork.GenericRepository<EmployeeRole>().GetByIdAsync(id);
            if (employeeRole != null)
            {
                await this.unitOfWork.GenericRepository<EmployeeRole>().DeleteAsync(employeeRole);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task<EmployeeRoleViewModel> GetEmployeeRoleById(int id)
        {
            var employeeRoleDb = await this.unitOfWork.GenericRepository<EmployeeRole>().GetByIdAsync(id);
            if (employeeRoleDb != null)
            {
                var employeeRole = new EmployeeRoleViewModel()
                {
                    Id = employeeRoleDb.Id,
                    EmployeeId = employeeRoleDb.EmployeeId,
                    Role = employeeRoleDb.Role
                };
                return employeeRole;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public IEnumerable<EmployeeRoleViewModel> GetAllEmployeeRoles()
        {
            var employeeRoles = new List<EmployeeRoleViewModel>();
            var employeeRolesDb = this.unitOfWork.GenericRepository<EmployeeRole>().GetAll();
            if (employeeRolesDb != null)
            {
                foreach(var employeeRole in employeeRolesDb)
                {
                    var currentEmployeeRole = new EmployeeRoleViewModel()
                    {
                        Id = employeeRole.Id,
                        EmployeeId = employeeRole.EmployeeId,
                        Role = employeeRole.Role
                    };
                    employeeRoles.Add(currentEmployeeRole);
                }
                return employeeRoles;
            }
            else
            {
                throw new Exception("employee roles doesn't exist");
            }
        }
    }
}