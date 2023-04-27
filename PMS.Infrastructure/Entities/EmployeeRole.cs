using Newtonsoft.Json;

namespace PMS.Infrastructure.Entities
{
    public class EmployeeRole : BaseEntity
    {
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public string Role {get;set;}
        public Employee Employee {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}