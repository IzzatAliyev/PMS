using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.EmployeeProject;

namespace PMS.Service.Services.Impl
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeeProjectService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateEmployeeProject(EmployeeProjectViewModel employeeProject)
        {
            var newEmployeeProject = new EmployeeProject()
            {
                Id = employeeProject.Id,
                EmployeeId = employeeProject.EmployeeId,
                ProjectId = employeeProject.ProjectId,
                Task = employeeProject.Task
            };
            await this.unitOfWork.GenericRepository<EmployeeProject>().AddAsync(newEmployeeProject);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateEmployeeProjectById(int id, EmployeeProjectViewModel employeeProject)
        {
            var employeeProjectOld = await this.unitOfWork.GenericRepository<EmployeeProject>().GetByIdAsync(id);
            if (employeeProjectOld != null)
            {
                employeeProjectOld.EmployeeId = employeeProject.EmployeeId;
                employeeProjectOld.ProjectId = employeeProject.ProjectId;
                employeeProjectOld.Task = employeeProject.Task;
                await this.unitOfWork.GenericRepository<EmployeeProject>().UpdateAsync(employeeProjectOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteEmployeeProjectById(int id)
        {
            var employeeProject = await this.unitOfWork.GenericRepository<EmployeeProject>().GetByIdAsync(id);
            if (employeeProject != null)
            {
                await this.unitOfWork.GenericRepository<EmployeeProject>().DeleteAsync(employeeProject);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task<EmployeeProjectViewModel> GetEmployeeProjectById(int id)
        {
            var employeeProjectDb = await this.unitOfWork.GenericRepository<EmployeeProject>().GetByIdAsync(id);
            if (employeeProjectDb != null)
            {
                var employeeProject = new EmployeeProjectViewModel()
                {
                    Id = employeeProjectDb.Id,
                    EmployeeId = employeeProjectDb.EmployeeId,
                    ProjectId = employeeProjectDb.ProjectId,
                    Task = employeeProjectDb.Task
                };
                return employeeProject;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public IEnumerable<EmployeeProjectViewModel> GetAllEmployeeProjects()
        {
            var employeeProjects = new List<EmployeeProjectViewModel>();
            var employeeProjectsDb = this.unitOfWork.GenericRepository<EmployeeProject>().GetAll();
            if (employeeProjectsDb != null)
            {
                foreach(var employeeProject in employeeProjectsDb)
                {
                    var currentEmployeeProject = new EmployeeProjectViewModel()
                    {
                        Id = employeeProject.Id,
                        EmployeeId = employeeProject.EmployeeId,
                        ProjectId = employeeProject.ProjectId,
                        Task = employeeProject.Task
                    };
                    employeeProjects.Add(currentEmployeeProject);
                }
                return employeeProjects;
            }
            else
            {
                throw new Exception("employee projects doesn't exist");
            }
        }
    }
}