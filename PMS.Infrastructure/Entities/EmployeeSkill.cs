using Newtonsoft.Json;

namespace PMS.Infrastructure.Entities
{
    public class EmployeeSkill
    {
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public int SkillId {get;set;}
        public int Level {get;set;}
        public Employee Employee {get;set;}
        public Skill Skill {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}