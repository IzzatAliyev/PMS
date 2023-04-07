using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Project;

namespace PMS.Service.Services.Impl
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateProject(ProjectViewModel project)
        {
            var newProject = new Project()
            {
                Name = project.Name,
                Description = project.Description,
                Status = project.Status
            };
            await this.unitOfWork.GenericRepository<Project>().AddAsync(newProject);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateProjectById(int id, ProjectViewModel project)
        {
            var projectOld = await this.unitOfWork.GenericRepository<Project>().GetByIdAsync(id);
            if (projectOld != null)
            {
                projectOld.Name = project.Name;
                projectOld.Description = project.Description;
                projectOld.Status = project.Status;
                await this.unitOfWork.GenericRepository<Project>().UpdateAsync(projectOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteProjectById(int id)
        {
            var project = await this.unitOfWork.GenericRepository<Project>().GetByIdAsync(id);
            if (project != null)
            {
                await this.unitOfWork.GenericRepository<Project>().DeleteAsync(project);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task<ProjectViewModel> GetProjectById(int id)
        {
            var projectDb = await this.unitOfWork.GenericRepository<Project>().GetByIdAsync(id);
            if (projectDb != null)
            {
                var project = new ProjectViewModel()
                {
                    Id = projectDb.Id,
                    Name = projectDb.Name,
                    Description = projectDb.Description,
                    Status = projectDb.Status
                };
                return project;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public IEnumerable<ProjectViewModel> GetAllProjects()
        {
            var projects = new List<ProjectViewModel>();
            var projectsDb = this.unitOfWork.GenericRepository<Project>().GetAll();
            if (projectsDb != null)
            {
                foreach(var project in projectsDb)
                {
                    var currentProject = new ProjectViewModel()
                    {
                        Id = project.Id,
                        Name = project.Name,
                        Description = project.Description,
                        Status = project.Status
                    };
                    projects.Add(currentProject);
                }
                return projects;
            }
            else
            {
                throw new Exception("projects doesn't exist");
            }
        }
    }
}