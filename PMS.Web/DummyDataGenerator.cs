using Bogus;
using PMS.Infrastructure.Enums;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeProject;
using PMS.Service.ViewModels.EmployeeRole;
using PMS.Service.ViewModels.EmployeeSkill;
using PMS.Service.ViewModels.Project;
using PMS.Service.ViewModels.ProjectRole;
using PMS.Service.ViewModels.PTask;
using PMS.Service.ViewModels.Skill;

namespace PMS.Web
{
    public static class DummyDataGenerator
    {
        public static async Task GenerateAsync(this IServiceProvider app)
        {
            var employeeIds = new List<int>();
            var projectIds = new List<int>();
            var skillIds = new List<int>();

            var employeeFaker = new EmployeeFaker();
            var projectFaker = new ProjectFaker();
            var skillFaker = new SkillFaker();
            var taskFaker = new TaskFaker();
            var employeeProjectaker = new EmployeeProjectFaker();
            var employeeRoleFaker = new EmployeeRoleFaker();
            var employeeSkillFaker = new EmployeeSkillFaker();
            var projectRoleFaker = new ProjectRoleFaker();

            using (var scope = app.CreateScope())
            {
                var employeeService = scope.ServiceProvider.GetService<IEmployeeService>();
                var projectService = scope.ServiceProvider.GetService<IProjectService>();
                var skillService = scope.ServiceProvider.GetService<ISkillService>();
                var taskService = scope.ServiceProvider.GetService<ITaskService>();
                var employeeProjectService = scope.ServiceProvider.GetService<IEmployeeProjectService>();
                var employeeRoleService = scope.ServiceProvider.GetService<IEmployeeRoleService>();
                var employeeSkillService = scope.ServiceProvider.GetService<IEmployeeSkillService>();
                var projectRoleService = scope.ServiceProvider.GetService<IProjectRoleService>();
                if (employeeService == null || projectService == null
                    || skillService == null || taskService == null
                    || employeeProjectService == null || employeeRoleService == null
                    || employeeSkillService == null || projectRoleService == null)
                {
                    throw new InvalidOperationException("services not found.");
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var employee = employeeFaker.Generate();
                        {
                            employeeIds.Add(employee.Id);
                        }
                        var project = projectFaker.Generate();
                        {
                            projectIds.Add(project.Id);
                        }
                        var skill = skillFaker.Generate();
                        {
                            skillIds.Add(skill.Id);
                        }

                        await employeeService.CreateEmployee(employee);
                        await projectService.CreateProject(project);
                        await skillService.CreateSkill(skill);
                    }

                    var task = taskFaker.Generate();
                    {
                        task.AssignedToId = employeeIds.Find(x => x % 2 == 0);
                        task.AssignedFromId = employeeIds.Find(x => x % 2 == 1);
                        task.ProjectId = projectIds.Find(x => x % 2 == 0);
                    }
                    await taskService.CreateTask(task);

                    var employeeProject = employeeProjectaker.Generate();
                    {
                        employeeProject.EmployeeId = employeeIds.Find(x => x % 2 == 0);
                        employeeProject.ProjectId = projectIds.Find(x => x % 2 == 0);
                    }
                    await employeeProjectService.CreateEmployeeProject(employeeProject);

                    var employeeRole = employeeRoleFaker.Generate();
                    {
                        employeeRole.EmployeeId = employeeIds.Find(x => x % 2 == 0);
                    }
                    await employeeRoleService.CreateEmployeeRole(employeeRole);

                    var employeeSkill = employeeSkillFaker.Generate();
                    {
                        employeeSkill.EmployeeId = employeeIds.Find(x => x % 2 == 0);
                        employeeSkill.SkillId = skillIds.Find(x => x % 2 == 0);
                    }
                    await employeeSkillService.CreateEmployeeSkill(employeeSkill);

                    var projectRole = projectRoleFaker.Generate();
                    {
                        projectRole.EmployeeId = employeeIds.Find(x => x % 2 == 0);
                        projectRole.ProjectId = projectIds.Find(x => x % 2 == 0);
                    }
                    await projectRoleService.CreateProjectRole(projectRole);
                }
            }
        }
    }

    public class EmployeeFaker : Faker<EmployeeViewModel>
    {
        public EmployeeFaker()
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 3);
            RuleFor(e => e.UserName, f => f.Person.FullName);
            RuleFor(e => e.FirstName, f => f.Person.FirstName);
            RuleFor(e => e.LastName, f => f.Person.LastName);
            RuleFor(e => e.Position, f => f.PickRandom<EmployeePosition>());
            RuleFor(e => e.Email, f => f.Person.Email);
            RuleFor(e => e.Description, f => f.Lorem.Sentences(1));
            RuleFor(e => e.PhoneNumber, f => f.Person.Phone);
            RuleFor(e => e.ProfilePicture, f => f.Internet.Avatar());
        }
    }

    public class ProjectFaker : Faker<ProjectViewModel>
    {
        public ProjectFaker()
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 1);
            RuleFor(e => e.Name, f => f.Lorem.Sentence(1));
            RuleFor(e => e.Description, f => f.Lorem.Sentences(1));
            RuleFor(e => e.Status, f => f.PickRandom<ProjectStatus>());
        }
    }

    public class SkillFaker : Faker<SkillViewModel>
    {
        public SkillFaker()
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 1);
            RuleFor(e => e.Name, f => f.Lorem.Sentence(1));
            RuleFor(e => e.Description, f => f.Lorem.Sentences(1));
            RuleFor(e => e.ColorCode, f => f.PickRandomParam("#ff0000", "#134000","#ff3543","#345433","#123123","#657656","#977643","#345432","#356811","#765432","#ccc455"));
        }
    }

    public class TaskFaker : Faker<PTaskViewModel>
    {
        public TaskFaker()
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 1);
            RuleFor(e => e.Name, f => f.Lorem.Sentence(1));
            RuleFor(e => e.Description, f => f.Lorem.Sentences(1));
            RuleFor(e => e.TaskType, f => f.PickRandom<PTaskType>());
            RuleFor(e => e.Status, f => f.PickRandom<PTaskStatus>());
        }
    }

    public class EmployeeProjectFaker : Faker<EmployeeProjectViewModel>
    {
        public EmployeeProjectFaker()
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 1);
            RuleFor(e => e.Task, f => f.PickRandom<EmployeeTask>());
        }
    }

    public class EmployeeRoleFaker : Faker<EmployeeRoleViewModel>
    {
        public EmployeeRoleFaker()
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 1);
            RuleFor(e => e.Role, f => f.Lorem.Sentence(1));
        }
    }

    public class EmployeeSkillFaker : Faker<EmployeeSkillViewModel>
    {
        public EmployeeSkillFaker()
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 1);
            RuleFor(e => e.Level, f => f.PickRandom<SkillLevel>());
        }
    }

    public class ProjectRoleFaker : Faker<ProjectRoleViewModel>
    {
        public ProjectRoleFaker()
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 1);
            RuleFor(e => e.Role, f => f.Lorem.Sentence(1));
        }
    }
}