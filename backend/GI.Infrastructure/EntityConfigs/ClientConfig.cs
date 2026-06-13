using GI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GI.Infrastructure.EntityConfigs
{
    internal class ClientConfig : BaseConfig<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Gstin).HasMaxLength(15);
            builder.Property(c => c.State).IsRequired().HasMaxLength(100);
            builder.Property(x => x.StateCode)
                .IsRequired();

            builder.HasIndex(x => new { x.UserId, x.Gstin }).IsUnique();
            builder.HasIndex(x => new { x.UserId, x.Email }).IsUnique();

            builder.HasOne(c => c.User)
             .WithMany(u => u.Clients)
             .HasForeignKey(c => c.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
