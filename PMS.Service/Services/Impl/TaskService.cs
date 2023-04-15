using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
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
                Name = task.Name,
                Description = task.Description,
                TaskType = task.TaskType,
                AssignedTo = task.AssignedTo,
                AssignedFrom = task.AssignedFrom,
                EmployeeId = task.EmployeeId,
                ProjectId = task.ProjectId,
                Status = task.Status
            };
            await this.unitOfWork.GenericRepository<PTask>().AddAsync(newTask);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateTaskById(int id, PTaskViewModel task)
        {
            var taskOld = await this.unitOfWork.GenericRepository<PTask>().GetByIdAsync(id);
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
    }
}