using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Entities
{
    public class EmployeeSkill : BaseEntity
    {
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public int SkillId {get;set;}
        public SkillLevel Level {get;set;}
        public Employee Employee {get;set;}
        public Skill Skill {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}