using Microsoft.EntityFrameworkCore;
using PMS.Core.Repositories;
using PMS.Infrastructure.Data;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeProject;
using PMS.Service.ViewModels.Project;

namespace PMS.Service.Services.Impl
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PMSDbContext dbcontext;

        public EmployeeProjectService(IUnitOfWork unitOfWork, PMSDbContext dbcontext)
        {
            this.unitOfWork = unitOfWork;
            this.dbcontext = dbcontext;
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

        public IEnumerable<ProjectViewModel> GetProjectsByEmployeeId(int employeeId)
        {
            var result = this.dbcontext.Employees
                .Include(e => e.Projects)
                .Where(e => e.Id == employeeId)
                .SelectMany(e => e.Projects.Select(p => new ProjectViewModel
                {
                    Id = p.Project.Id,
                    Name = p.Project.Name,
                    Description = p.Project.Description,
                    Status = p.Project.Status
                }))
                .ToList();

                return result;
        }

        public IEnumerable<EmployeeWithRoleViewModel> GetEmployeesByProjectId(int projectId)
        {
             var result = this.dbcontext.Projects
                .Include(p => p.Employees)
                .Where(p => p.Id == projectId)
                .SelectMany(p => p.Employees.Select(e => new EmployeeWithRoleViewModel
                {
                    Id = e.Employee.Id,
                    UserName = e.Employee.UserName,
                    FirstName = e.Employee.FirstName,
                    LastName = e.Employee.LastName,
                    Position = e.Employee.Position,
                    Email = e.Employee.Email,
                    Description = e.Employee.Description,
                    PhoneNumber = e.Employee.PhoneNumber,
                    ProfilePicture = e.Employee.ProfilePicture,
                    Role = e.Task
                }))
                .ToList();

                return result;
        }
    }
}