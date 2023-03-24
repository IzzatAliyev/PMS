namespace PMS.Core.Entities
{
    public class EmployeeProject
    {
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public int ProjectId {get;set;}
        public string Task {get;set;}
        public Employee Employee {get;set;}
        public Project Project {get;set;}
    }
}