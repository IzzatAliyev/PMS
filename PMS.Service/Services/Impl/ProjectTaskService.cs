using Microsoft.EntityFrameworkCore;
using PMS.Core.Repositories;
using PMS.Infrastructure.Data;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.ProjectTask;

namespace PMS.Service.Services.Impl
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PMSDbContext dbcontext;

        public ProjectTaskService(IUnitOfWork unitOfWork, PMSDbContext dbcontext)
        {
            this.unitOfWork = unitOfWork;
            this.dbcontext = dbcontext;
        }
        public async Task CreateTask(ProjectTaskViewModel task)
        {
            var newTask = new ProjectTask()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                TaskType = task.TaskType,
                AssignedTo = task.AssignedTo,
                AssignedFrom = task.AssignedFrom,
                EmployeeId = task.EmployeeId,
                ProjectId = task.ProjectId,
                Status = task.Status
            };
            await this.unitOfWork.GenericRepository<ProjectTask>().AddAsync(newTask);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateTaskById(int id, ProjectTaskViewModel task)
        {
            var taskOld = await this.unitOfWork.GenericRepository<ProjectTask>().GetByIdAsync(id);
            if (taskOld != null)
            {
                taskOld.Name = task.Name;
                taskOld.Description = task.Description;
                taskOld.TaskType = task.TaskType;
                taskOld.AssignedTo = task.AssignedTo;
                taskOld.AssignedFrom = task.AssignedFrom;
                taskOld.EmployeeId = task.EmployeeId;
                taskOld.ProjectId = task.ProjectId;
                taskOld.Status = task.Status;
                await this.unitOfWork.GenericRepository<ProjectTask>().UpdateAsync(taskOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteTaskById(int id)
        {
            var task = await this.unitOfWork.GenericRepository<ProjectTask>().GetByIdAsync(id);
            if (task != null)
            {
                await this.unitOfWork.GenericRepository<ProjectTask>().DeleteAsync(task);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task<ProjectTaskViewModel> GetTaskById(int id)
        {
            var taskDb = await this.unitOfWork.GenericRepository<ProjectTask>().GetByIdAsync(id);
            if (taskDb != null)
            {
                var task = new ProjectTaskViewModel()
                {
                    Id = taskDb.Id,
                    Name = taskDb.Name,
                    Description = taskDb.Description,
                    TaskType = taskDb.TaskType,
                    AssignedTo = taskDb.AssignedTo,
                    AssignedFrom = taskDb.AssignedFrom,
                    EmployeeId = taskDb.EmployeeId,
                    ProjectId = taskDb.ProjectId,
                    Status = taskDb.Status
                };
                return task;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public IEnumerable<ProjectTaskViewModel> GetAllTasks()
        {
            var tasks = new List<ProjectTaskViewModel>();
            var tasksDb = this.unitOfWork.GenericRepository<ProjectTask>().GetAll();
            if (tasksDb != null)
            {
                foreach(var task in tasksDb)
                {
                    var currentProject = new ProjectTaskViewModel()
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        TaskType = task.TaskType,
                        AssignedTo = task.AssignedTo,
                        AssignedFrom = task.AssignedFrom,
                        EmployeeId = task.EmployeeId,
                        ProjectId = task.ProjectId,
                        Status = task.Status
                    };
                    tasks.Add(currentProject);
                }
                return tasks;
            }
            else
            {
                throw new Exception("project tasks doesn't exist");
            }
        }

        public IEnumerable<ProjectTaskViewModel> GetTasksByProjectId(int projectId)
        {
            var result = this.dbcontext.Projects
                .Include(p => p.Tasks)
                .Where(p => p.Id == projectId)
                .SelectMany(p => p.Tasks.Select(t =>new ProjectTaskViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    TaskType = t.TaskType,
                    AssignedTo = t.AssignedTo,
                    AssignedFrom = t.AssignedFrom,
                    Status = t.Status,
                    EmployeeId = t.EmployeeId,
                    ProjectId = t.ProjectId
                }))
                .ToList();

                return result;
        }
    }
}