using Microsoft.AspNetCore.Identity;

namespace PMS.Infrastructure.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}