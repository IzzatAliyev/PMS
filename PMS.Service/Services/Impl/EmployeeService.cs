using PMS.Core.Repositories;
using PMS.Infrastructure.Data;
using PMS.Infrastructure.Entities;
using PMS.Infrastructure.Enums;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Employee;
using PMS.Service.ViewModels.EmployeeProject;

namespace PMS.Service.Services.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PMSDbContext context;

        public EmployeeService(IUnitOfWork unitOfWork, PMSDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }
        public async Task CreateEmployee(EmployeeViewModel employee)
        {
            var newEmployee = new Employee()
            {
                Id = employee.Id,
                UserName = employee.UserName != null ? employee.UserName : string.Empty,
                FirstName = employee.FirstName != null ? employee.FirstName : string.Empty,
                LastName = employee.LastName != null ? employee.LastName : string.Empty,
                Position = employee.Position != null ? employee.Position : EmployeePosition.Intern,
                Email = employee.Email != null ? employee.Email : string.Empty,
                Description = employee.Description != null ? employee.Description : string.Empty,
                PhoneNumber = employee.PhoneNumber != null ? employee.PhoneNumber : string.Empty,
                ProfilePicture = employee.ProfilePicture != null ? employee.ProfilePicture : string.Empty
            };
            await this.unitOfWork.GenericRepository<Employee>().AddAsync(newEmployee);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateEmployeeById(int id, EmployeeViewModel employee)
        {
            var employeeOld = await this.unitOfWork.GenericRepository<Employee>().GetByIdAsync(id);
            if (employeeOld != null)
            {
                employeeOld.UserName = employee.UserName != null ? employee.UserName : employeeOld.UserName;
                employeeOld.FirstName = employee.FirstName != null ? employee.FirstName : employeeOld.FirstName;
                employeeOld.LastName = employee.LastName != null ? employee.LastName : employeeOld.LastName;
                employeeOld.Position = employee.Position != null ? employee.Position : employeeOld.Position;
                employeeOld.Email = employee.Email != null ? employee.Email : employeeOld.Email;
                employeeOld.Description = employee.Description != null ? employee.Description : employeeOld.Description;
                employeeOld.PhoneNumber = employee.PhoneNumber != null ? employee.PhoneNumber : employeeOld.PhoneNumber;
                employeeOld.ProfilePicture = employee.ProfilePicture != null ? employee.ProfilePicture : employeeOld.ProfilePicture;
                employeeOld.UpdatedAt = DateTime.UtcNow;
                await this.unitOfWork.GenericRepository<Employee>().UpdateAsync(employeeOld);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task DeleteEmployeeById(int id)
        {
            var employee = await this.unitOfWork.GenericRepository<Employee>().GetByIdAsync(id);
            if (employee != null)
            {
                await this.unitOfWork.GenericRepository<Employee>().DeleteAsync(employee);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public async Task<EmployeeViewModel> GetEmployeeById(int id)
        {
            var employeeDb = await this.unitOfWork.GenericRepository<Employee>().GetByIdAsync(id);
            if (employeeDb != null)
            {
                var employee = new EmployeeViewModel()
                {
                    Id = employeeDb.Id,
                    UserName = employeeDb.UserName,
                    FirstName = employeeDb.FirstName,
                    LastName = employeeDb.LastName,
                    Position = employeeDb.Position,
                    Email = employeeDb.Email,
                    Description = employeeDb.Description,
                    PhoneNumber = employeeDb.PhoneNumber,
                    ProfilePicture = employeeDb.ProfilePicture
                };
                return employee;
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }

        public EmployeeViewModel GetEmployeeByEmail(string email)
        {
            var employeeDb = this.context.Employees.Where(e => e.Email == email).First();
            if (employeeDb != null)
            {
                var employee = new EmployeeViewModel()
                {
                    Id = employeeDb.Id,
                    UserName = employeeDb.UserName,
                    FirstName = employeeDb.FirstName,
                    LastName = employeeDb.LastName,
                    Position = employeeDb.Position,
                    Email = employeeDb.Email,
                    Description = employeeDb.Description,
                    PhoneNumber = employeeDb.PhoneNumber,
                    ProfilePicture = employeeDb.ProfilePicture
                };
                return employee;
            }
            else
            {
                throw new Exception("email doesn't exist");
            }
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployees()
        {
            var employees = new List<EmployeeViewModel>();
            var employeesDb = this.unitOfWork.GenericRepository<Employee>().GetAll();
            if (employeesDb != null)
            {
                foreach(var employee in employeesDb)
                {
                    var currentEmployee = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        UserName = employee.UserName,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Position = employee.Position,
                        Email = employee.Email,
                        Description = employee.Description,
                        PhoneNumber = employee.PhoneNumber,
                        ProfilePicture = employee.ProfilePicture
                    };
                    employees.Add(currentEmployee);
                }
                return employees;
            }
            else
            {
                throw new Exception("employees doesn't exist");
            }
        }

        public IEnumerable<EmployeeProjectSearchViewModel> GetEmployeesBySearchInput(string input)
        {
            var matchingEmployees = this.context.Employees
                .Where(e => e.UserName.ToLower().Contains(input) 
                || e.FirstName.ToLower().Contains(input) 
                || e.LastName.ToLower().Contains(input))
                .Select(e => new EmployeeProjectSearchViewModel
                {
                    Name = e.UserName,
                    Url = $"http://localhost:5047/employees/{e.Id}",
                    Type = "employee"
                })
                .ToList();

            return matchingEmployees;
        }

        public async Task SetProfilePictureByEmployeeId(int id, string url)
        {
            var employeeDb = await this.unitOfWork.GenericRepository<Employee>().GetByIdAsync(id);
            if (employeeDb != null)
            {
                employeeDb.ProfilePicture = url != null ? url : employeeDb.ProfilePicture;
                await this.unitOfWork.GenericRepository<Employee>().UpdateAsync(employeeDb);
                await this.unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("id doesn't exist");
            }
        }
    }
}