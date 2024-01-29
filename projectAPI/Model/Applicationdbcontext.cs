using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace projectAPI.Model
{
    public class Applicationdbcontext:IdentityDbContext<ApplicationUser>
    {
        public Applicationdbcontext(DbContextOptions<Applicationdbcontext>Options):base(Options)
        {
        }

        public DbSet <Genre> genres { get; set; }
        public DbSet <Movie> movies { get; set; }
    }
}
