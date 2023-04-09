using Newtonsoft.Json;

namespace PMS.Infrastructure.Entities
{
    public class Skill
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public string ColorCode {get;set;}
        public virtual ICollection<EmployeeSkill> Employees {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}