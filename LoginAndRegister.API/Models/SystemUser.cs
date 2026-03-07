using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoginAndRegister.API.Models
{
    public class SystemUser : IdentityUser
    {
        [Key]
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public required string Phone { get; set; }
    }
}