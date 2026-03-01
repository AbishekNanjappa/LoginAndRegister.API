using Microsoft.AspNetCore.Identity;

namespace LoginAndRegister.API.Models
{
    public class SystemUser: IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
