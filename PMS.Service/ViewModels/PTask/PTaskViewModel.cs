using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Service.ViewModels.PTask
{
    public class PTaskViewModel
    {
        public int Id {get;set;}
        public string? Name {get;set;}
        public string? Description {get;set;}
        public PTaskType TaskType {get;set;}
        public int AssignedToId {get;set;}
        public int AssignedFromId {get;set;}
        public PTaskStatus Status {get;set;}
        public int ProjectId {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}