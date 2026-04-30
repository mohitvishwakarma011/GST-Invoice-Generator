using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GI.Infrastructure.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<User>
    }
}
