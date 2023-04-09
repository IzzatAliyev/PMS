using Newtonsoft.Json;

namespace PMS.Service.ViewModels.EmployeeRole
{
    public class EmployeeRoleViewModel
    {
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public string Role {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}