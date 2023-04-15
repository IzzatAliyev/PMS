using Microsoft.EntityFrameworkCore;
using PMS.Core.Repositories;
using PMS.Infrastructure.Data;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.EmployeeProject;
using PMS.Service.ViewModels.Project;
using PMS.Service.ViewModels.PTask;

namespace PMS.Service.Services.Impl
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PMSDbContext context;

        public ProjectService(IUnitOfWork unitOfWork, PMSDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }
        public async Task CreateProject(ProjectViewModel project)
        {
            var newProject = new Project()
            {
                Id = project.Id,
                Name = project.Name != null ? project.Name : string.Empty,
                Description = project.Description != null ? project.Description : string.Empty,
                Status = project.Status != null ? project.Status : string.Empty
            };
            await this.unitOfWork.GenericRepository<Project>().AddAsync(newProject);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateProjectById(int id, ProjectViewModel project)
        {
            var projectOld = await this.unitOfWork.GenericRepository<Project>().GetByIdAsync(id);
            if (projectOld != null)
            {
                projectOld.Name = project.Name != null ? project.Name : projectOld.Name;
                projectOld.Description = project.Description != null ? project.Description : projectOld.Description;
                projectOld.Status = project.Status != null ? project.Status : projectOld.Status;
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

        public IEnumerable<EmployeeProjectSearchViewModel> GetProjectsBySearchInput(string input)
        {
            var matchingProjects = this.context.Projects
                .Where(p => p.Name.ToLower().Contains(input) || p.Description.ToLower().Contains(input))
                .Select(p => new EmployeeProjectSearchViewModel
                {
                    Name = p.Name,
                    Url = $"http://localhost:5047/projects/{p.Id}",
                    Type = "project"
                })
                .ToList();
            return matchingProjects;
        }

        public IEnumerable<PTaskViewModel> GetTasksByProjectId(int projectId)
        {
            var result = this.context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.Id == projectId)
                .SelectMany(p => p.Tasks.Select(t =>new PTaskViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    TaskType = t.TaskType,
                    AssignedToId = t.AssignedToId,
                    AssignedFromId = t.AssignedFromId,
                    Status = t.Status,
                    ProjectId = t.ProjectId
                }))
                .ToList();

                return result;
        }
    }
}