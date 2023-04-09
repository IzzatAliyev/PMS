using Newtonsoft.Json;

namespace PMS.Service.ViewModels.EmployeeSkill
{
    public class EmployeeSkillViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        public int Level { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}