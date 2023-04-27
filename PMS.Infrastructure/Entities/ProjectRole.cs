using Newtonsoft.Json;

namespace PMS.Infrastructure.Entities
{
    public class ProjectRole : BaseEntity
    {
        public int Id {get;set;}
        public int ProjectId {get;set;}
        public int EmployeeId {get;set;}
        public string Role {get;set;}
        public Project Project {get;set;}
        public Employee Employee {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}