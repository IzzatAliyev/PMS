using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Service.ViewModels.EmployeeSkill
{
    public class EmployeeSkillViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        public SkillLevel Level { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}