using Newtonsoft.Json;
using PMS.Infrastructure.Enums;

namespace PMS.Infrastructure.Entities
{
    public class Employee : BaseEntity
    {
        public int Id {get;set;}
        public string UserName {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public EmployeePosition Position {get;set;}
        public string Email {get;set;}
        public string Description {get;set;}
        public string PhoneNumber {get;set;}
        public string ProfilePicture {get;set;}
        public virtual ICollection<EmployeeProject> Projects {get;set;}
        public virtual ICollection<EmployeeSkill> Skills {get;set;}
        public virtual ICollection<PTask> TasksTo {get;set;}
        public virtual ICollection<PTask> TasksFrom {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}