using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.EmployeeProject;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/employee-project")]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectService employeeProjectService;
        private readonly IEmployeeService employeeService;
        private readonly IProjectService projectService;
        private readonly IMemoryCache cache;
        private readonly ILogger<EmployeeProjectController> logger;

        public EmployeeProjectController(IEmployeeProjectService employeeProjectService, IEmployeeService employeeService, IProjectService projectService, IMemoryCache cache, ILogger<EmployeeProjectController> logger)
        {
            this.employeeProjectService = employeeProjectService;
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.cache = cache;
            this.logger = logger;
        }

        [HttpGet("project/{projectId:int}/employees")]
        public IActionResult GetEmployeesByProjectId([FromRoute] int projectId)
        {
            if (projectId == 0)
            {
                return this.BadRequest("NotFound");
            }
            var employees = this.employeeProjectService.GetEmployeesByProjectId(projectId);
            return this.Ok(employees);
        }

        [HttpGet("employee/{employeeId:int}/projects")]
        public IActionResult GetProjectsByEmployeeId([FromRoute] int employeeId)
        {
            if (employeeId == 0)
            {
                return this.BadRequest("NotFound");
            }
            var projects = this.employeeProjectService.GetProjectsByEmployeeId(employeeId);
            return this.Ok(projects);
        }

        [HttpGet("search")]
        public IActionResult SearchEmployeesAndProjects([FromQuery] string query)
        {
            var queryLowerCase = query.ToLower();

            if (cache.TryGetValue(queryLowerCase, out List<EmployeeProjectSearchViewModel> cachedResults))
            {
                return this.Ok(cachedResults);
            }

            var matchingProjects = this.projectService.GetProjectsBySearchInput(queryLowerCase);
            var matchingEmployees = this.employeeService.GetEmployeesBySearchInput(queryLowerCase);

            var suitableData = matchingProjects.Concat(matchingEmployees).ToList();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            cache.Set(queryLowerCase, suitableData, cacheEntryOptions);

            return this.Ok(suitableData);
        }
    }
}