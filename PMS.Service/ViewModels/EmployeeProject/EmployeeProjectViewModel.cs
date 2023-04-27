using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Service.ViewModels.EmployeeProject
{
    public class EmployeeProjectViewModel
    {
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public int ProjectId {get;set;}
        public EmployeeTask Task {get;set;}
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}