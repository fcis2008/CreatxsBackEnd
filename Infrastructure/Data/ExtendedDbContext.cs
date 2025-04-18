using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ExtendedDbContext : IdentityDbContext<ApplicationUser>
    {
        public ExtendedDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
