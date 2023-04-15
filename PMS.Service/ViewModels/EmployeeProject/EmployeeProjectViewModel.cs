using Newtonsoft.Json;

namespace PMS.Service.ViewModels.EmployeeProject
{
    public class EmployeeProjectViewModel
    {
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public int ProjectId {get;set;}
        public string Task {get;set;}
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}