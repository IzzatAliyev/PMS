using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Entities
{
    public class EmployeeProject : BaseEntity
    {
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public int ProjectId {get;set;}
        public EmployeeTask Task {get;set;}
        public Employee Employee {get;set;}
        public Project Project {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}