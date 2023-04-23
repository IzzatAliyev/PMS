using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.PTask;

namespace PMS.Service.Services.Impl
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateTask(PTaskViewModel task)
        {
            var newTask = new PTask()
            {
                Id = task.Id,
                Name = task.Name != null ? task.Name : string.Empty,
                Description = task.Description != null ? task.Description : string.Empty,
                TaskType = task.TaskType != null ? task.TaskType : PTaskType.Testing,
                AssignedToId = task.AssignedToId,
                AssignedFromId = task.AssignedFromId,
                ProjectId = task.ProjectId,
                Status = task.Status != null ? task.Status : PTaskStatus.Todo
            };
            await this.unitOfWork.GenericRepository<PTask>().AddAsync(newTask);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateTaskById(int id, PTaskViewModel task)
        {
            var taskOld = await this.unitOfWork.GenericRepository<PTask>().GetByIdAsync(id);
            if (taskOld != null)
            {
                taskOld.Name = task.Name != null ? task.Name : taskOld.Name;
                taskOld.Description = task.Description != null ? task.Description : taskOld.Description;
                taskOld.TaskType = task.TaskType != null ? task.TaskType : taskOld.TaskType;
                taskOld.AssignedToId = task.AssignedToId != 0 ? task.AssignedToId : taskOld.AssignedToId;
                taskOld.AssignedFromId = task.AssignedFromId != 0 ? task.AssignedFromId : taskOld.AssignedFromId;
                taskOld.ProjectId = task.ProjectId != 0 ? task.ProjectId : taskOld.ProjectId;
                taskOld.Status = task.Status != null ? task.Status : taskOld.Status;
                taskOld.UpdatedAt = DateTime.UtcNow;
                await this.unitOfWork.GenericRepository<PTask>().UpdateAsync(taskOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteTaskById(int id)
        {
            var task = await this.unitOfWork.GenericRepository<PTask>().GetByIdAsync(id);
            if (task != null)
            {
                await this.unitOfWork.GenericRepository<PTask>().DeleteAsync(task);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task<PTaskViewModel> GetTaskById(int id)
        {
            var taskDb = await this.unitOfWork.GenericRepository<PTask>().GetByIdAsync(id);
            if (taskDb != null)
            {
                var task = new PTaskViewModel()
                {
                    Id = taskDb.Id,
                    Name = taskDb.Name,
                    Description = taskDb.Description,
                    TaskType = taskDb.TaskType,
                    AssignedToId = taskDb.AssignedToId,
                    AssignedFromId = taskDb.AssignedFromId,
                    ProjectId = taskDb.ProjectId,
                    Status = taskDb.Status,
                    CreatedAt = taskDb.CreatedAt,
                    UpdatedAt = taskDb.UpdatedAt
                };
                return task;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public IEnumerable<PTaskViewModel> GetAllTasks()
        {
            var tasks = new List<PTaskViewModel>();
            var tasksDb = this.unitOfWork.GenericRepository<PTask>().GetAll();
            if (tasksDb != null)
            {
                foreach(var task in tasksDb)
                {
                    var currentProject = new PTaskViewModel()
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        TaskType = task.TaskType,
                        AssignedToId = task.AssignedToId,
                        AssignedFromId = task.AssignedFromId,
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
    }
}