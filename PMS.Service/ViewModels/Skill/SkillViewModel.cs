using Newtonsoft.Json;

namespace PMS.Service.ViewModels.Skill
{
    public class SkillViewModel
    {
        public int Id {get;set;}
        public string? Name {get;set;}
        public string? Description {get;set;}
        public string? ColorCode {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}