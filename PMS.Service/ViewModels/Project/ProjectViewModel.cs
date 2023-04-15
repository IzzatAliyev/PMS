using Newtonsoft.Json;

namespace PMS.Service.ViewModels.Project
{
    public class ProjectViewModel
    {
        public int Id {get;set;}
        public string? Name {get;set;}
        public string? Description {get;set;}
        public string? Status {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}