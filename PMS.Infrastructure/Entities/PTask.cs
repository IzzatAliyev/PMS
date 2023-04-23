using Newtonsoft.Json;

namespace PMS.Infrastructure.Entities
{
    public class PTask : BaseEntity
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public string TaskType {get;set;}
        public int AssignedToId {get;set;}
        public int AssignedFromId {get;set;}
        public string Status {get;set;}
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