using LoginAndRegister.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoginAndRegister.API.DatabaseContext
{
    public class ApplicationDbContext: Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<SystemUser, 
        IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
