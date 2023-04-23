using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Entities
{
    public class Project : BaseEntity
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public ProjectStatus Status {get;set;}
        public virtual ICollection<EmployeeProject> Employees {get;set;}
        public virtual ICollection<PTask> Tasks {get;set;}
        public ProjectRole Role {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}