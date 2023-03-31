namespace PMS.Infrastructure.Entities
{
    public class ProjectRole
    {
        public int Id {get;set;}
        public int ProjectId {get;set;}
        public int EmployeeId {get;set;}
        public string Role {get;set;}
        public Project Project {get;set;}
        public Employee Employee {get;set;}
    }
}