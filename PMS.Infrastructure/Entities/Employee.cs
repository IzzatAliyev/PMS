namespace PMS.Infrastructure.Entities
{
    public class Employee
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Position {get;set;}
        public string Email {get;set;}
        public string Description {get;set;}
        public string PhoneNumber {get;set;}
        public string ProfilePicture {get;set;}
        public virtual ICollection<EmployeeProject> Projects {get;set;}
        public virtual ICollection<EmployeeSkill> Skills {get;set;}
        public virtual ICollection<ProjectTask> Tasks {get;set;}
    }
}