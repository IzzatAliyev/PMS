using PMS.Core.Repositories;
using PMS.Infrastructure.Entities;
using PMS.Service.Services.Interfaces;
using PMS.Service.ViewModels.Employee;

namespace PMS.Service.Services.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateEmployee(EmployeeViewModel employee)
        {
            var newEmployee = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Email = employee.Email,
                Description = employee.Description,
                PhoneNumber = employee.PhoneNumber,
                ProfilePicture = employee.ProfilePicture
            };
            await this.unitOfWork.GenericRepository<Employee>().AddAsync(newEmployee);
            await this.unitOfWork.SaveAsync();
        }

        public async Task UpdateEmployeeById(int id, EmployeeViewModel employee)
        {
            var employeeOld = await this.unitOfWork.GenericRepository<Employee>().GetByIdAsync(id);
            if (employeeOld != null)
            {
                employeeOld.Name = employee.Name;
                employeeOld.Position = employee.Position;
                employeeOld.Email = employee.Email;
                employeeOld.Description = employee.Description;
                employeeOld.PhoneNumber = employee.PhoneNumber;
                employeeOld.ProfilePicture = employee.ProfilePicture;
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
                    Name = employeeDb.Name,
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
                        Name = employee.Name,
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
    }
}