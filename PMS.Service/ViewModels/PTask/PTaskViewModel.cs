using Newtonsoft.Json;

namespace PMS.Service.ViewModels.PTask
{
    public class PTaskViewModel
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public string TaskType {get;set;}
        public string AssignedTo {get;set;}
        public string AssignedFrom {get;set;}
        public string Status {get;set;}
        public int EmployeeId {get;set;}
        public int ProjectId {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}