using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Service.ViewModels.Project
{
    public class ProjectViewModel
    {
        public int Id {get;set;}
        public string? Name {get;set;}
        public string? Description {get;set;}
        public ProjectStatus Status {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}