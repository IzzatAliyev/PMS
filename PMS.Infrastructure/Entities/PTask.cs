using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Entities
{
    public class PTask : BaseEntity
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public PTaskType TaskType {get;set;}
        public int AssignedToId {get;set;}
        public int AssignedFromId {get;set;}
        public PTaskStatus Status {get;set;}
        public int ProjectId {get;set;}
        public Employee AssignedTo {get;set;}
        public Employee AssignedFrom {get;set;}
        public Project Project {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}