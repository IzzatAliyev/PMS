using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.Project;

namespace PMS.Web.Controllers
{
    [Authorize]
    [Route("admin")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("projects")]
        public async Task<IActionResult> ProjectsAsync(int page = 1, int pageSize = 10, string sortBy = "Id", bool sortDescending = false)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync("projects");
                var projects = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectViewModel>>();
                if (projects != null)
                {
                    if (sortDescending)
                    {
                        projects = sortBy switch
                        {
                            "Id" => projects.OrderByDescending(p => p.Id),
                            "Name" => projects.OrderByDescending(p => p.Name),
                            "Status" => projects.OrderByDescending(p => p.Status),
                            "Description" => projects.OrderByDescending(p => p.Description),
                            _ => projects.OrderByDescending(p => p.Id)
                        };
                    }
                    else
                    {
                        projects = sortBy switch
                        {
                            "Id" => projects.OrderBy(p => p.Id),
                            "Name" => projects.OrderBy(p => p.Name),
                            "Status" => projects.OrderBy(p => p.Status),
                            "Description" => projects.OrderBy(p => p.Description),
                            _ => projects.OrderBy(p => p.Id)
                        };
                    }

                    var totalItems = projects.Count();
                    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    var itemsToDisplay = projects.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    var viewModel = new MyViewModel<ProjectViewModel>
                    {
                        Items = itemsToDisplay,
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalPages = totalPages
                    };

                    ViewBag.SortBy = sortBy;
                    ViewBag.SortDescending = sortDescending;
                    return View(viewModel);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }
        }


        [HttpGet("employees")]
        public async Task<IActionResult> EmployeesAsync(int page = 1, int pageSize = 10, string sortBy = "Id", bool sortDescending = false)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5108/api/");
                var response = await client.GetAsync("employees");
                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>();
                if (employees != null)
                {
                    if (sortDescending)
                    {
                        employees = sortBy switch
                        {
                            "Id" => employees.OrderByDescending(p => p.Id),
                            "UserName" => employees.OrderByDescending(p => p.UserName),
                            "Email" => employees.OrderByDescending(p => p.Email),
                            "Position" => employees.OrderByDescending(p => p.Position),
                            _ => employees.OrderByDescending(p => p.Id)
                        };
                    }
                    else
                    {
                        employees = sortBy switch
                        {
                            "Id" => employees.OrderBy(p => p.Id),
                            "UserName" => employees.OrderBy(p => p.UserName),
                            "Email" => employees.OrderBy(p => p.Email),
                            "Position" => employees.OrderBy(p => p.Position),
                            _ => employees.OrderBy(p => p.Id)
                        };
                    }

                    var totalItems = employees.Count();
                    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    var itemsToDisplay = employees.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    var viewModel = new MyViewModel<EmployeeViewModel>
                    {
                        Items = itemsToDisplay,
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalPages = totalPages
                    };

                    ViewBag.SortBy = sortBy;
                    ViewBag.SortDescending = sortDescending;
                    return View(viewModel);
                }
                else
                {
                    return this.RedirectToAction("NotFound", "Home");
                }
            }

        }
    }

    public class MyViewModel<T>
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}