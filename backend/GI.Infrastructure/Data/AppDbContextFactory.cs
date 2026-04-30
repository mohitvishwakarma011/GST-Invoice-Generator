using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GI.Infrastructure.Data
{
    internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionBUilder = new DbContextOptionsBuilder<AppDbContext>();

            optionBUilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GstInvoice;Trusted_Connection=true;TrustServerCertificate=true");

            return new AppDbContext(optionBUilder.Options);
        }
    }
}
