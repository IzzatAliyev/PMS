using Newtonsoft.Json;

namespace PMS.Service.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public int Id {get;set;}
        public string? Name {get;set;}
        public string? Position {get;set;}
        public string? Email {get;set;}
        public string? Description {get;set;}
        public string? PhoneNumber {get;set;}
        public string? ProfilePicture {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}