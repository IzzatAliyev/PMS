using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.ProjectTask;

namespace PMS.Service.Services.Impl
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProjectTaskService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateTask(ProjectTaskViewModel task)
        {
            var newTask = new ProjectTask()
            {
                Name = task.Name,
                Description = task.Description,
                TaskType = task.TaskType,
                AssignedTo = task.AssignedTo,
                AssignedFrom = task.AssignedFrom,
                EmployeeId = task.EmployeeId,
                ProjectId = task.ProjectId
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
                    ProjectId = taskDb.ProjectId
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
                        ProjectId = task.ProjectId
                    };
                    tasks.Add(currentProject);
                }
                return tasks;
            }
            else
            {
                throw new Exception("projects doesn't exist");
            }
        }
    }
}