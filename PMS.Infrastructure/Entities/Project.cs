namespace PMS.Infrastructure.Entities
{
    public class Project
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public string Status {get;set;}
        public virtual ICollection<EmployeeProject> Employees {get;set;}
        public virtual ICollection<ProjectTask> Tasks {get;set;}
        public ProjectRole Role {get;set;}
    }
}