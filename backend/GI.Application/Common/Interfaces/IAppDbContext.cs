using GI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<InvoiceItem> InvoiceItems { get; set; }
        DbSet<State> States { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
