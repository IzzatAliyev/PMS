using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.ProjectRole;

namespace PMS.Service.Services.Impl
{
    public class ProjectRoleService : IProjectRoleService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProjectRoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateProjectRole(ProjectRoleViewModel projectRole)
        {
            var newProjectRole = new ProjectRole()
            {
                Id = projectRole.Id,
                EmployeeId = projectRole.EmployeeId,
                ProjectId = projectRole.ProjectId,
                Role = projectRole.Role
            };
            await this.unitOfWork.GenericRepository<ProjectRole>().AddAsync(newProjectRole);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateProjectRoleById(int id, ProjectRoleViewModel projectRole)
        {
            var projectRoleOld = await this.unitOfWork.GenericRepository<ProjectRole>().GetByIdAsync(id);
            if (projectRoleOld != null)
            {
                projectRoleOld.EmployeeId = projectRole.EmployeeId;
                projectRoleOld.ProjectId = projectRole.ProjectId;
                projectRoleOld.Role = projectRole.Role;
                projectRoleOld.UpdatedAt = DateTime.UtcNow;
                await this.unitOfWork.GenericRepository<ProjectRole>().UpdateAsync(projectRoleOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteProjectRoleById(int id)
        {
            var projectRole = await this.unitOfWork.GenericRepository<ProjectRole>().GetByIdAsync(id);
            if (projectRole != null)
            {
                await this.unitOfWork.GenericRepository<ProjectRole>().DeleteAsync(projectRole);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task<ProjectRoleViewModel> GetProjectRoleById(int id)
        {
            var projectRoleDb = await this.unitOfWork.GenericRepository<ProjectRole>().GetByIdAsync(id);
            if (projectRoleDb != null)
            {
                var projectRole = new ProjectRoleViewModel()
                {
                    Id = projectRoleDb.Id,
                    EmployeeId = projectRoleDb.EmployeeId,
                    ProjectId = projectRoleDb.ProjectId,
                    Role = projectRoleDb.Role
                };
                return projectRole;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public IEnumerable<ProjectRoleViewModel> GetAllProjectRoles()
        {
            var projectRoles = new List<ProjectRoleViewModel>();
            var projectRolesDb = this.unitOfWork.GenericRepository<ProjectRole>().GetAll();
            if (projectRolesDb != null)
            {
                foreach(var projectRole in projectRolesDb)
                {
                    var currentProjectRole = new ProjectRoleViewModel()
                    {
                        Id = projectRole.Id,
                        EmployeeId = projectRole.EmployeeId,
                        ProjectId = projectRole.ProjectId,
                        Role = projectRole.Role
                    };
                    projectRoles.Add(currentProjectRole);
                }
                return projectRoles;
            }
            else
            {
                throw new Exception("project roles doesn't exist");
            }
        }
    }
}