using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoginAndRegister.API.Models
{
    public class SystemUser : IdentityUser
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public required string Phone { get; set; }
    }
}