using Newtonsoft.Json;

namespace PMS.Service.ViewModels.ProjectRole
{
    public class ProjectRoleViewModel
    {
        public int Id {get;set;}
        public int ProjectId {get;set;}
        public int EmployeeId {get;set;}
        public string Role {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}