using GI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GI.Infrastructure.EntityConfigs
{
    internal class InvoiceConfig : BaseConfig<Invoice>
    {
        public override void Configure(EntityTypeBuilder<Invoice> builder)
        {
            base.Configure(builder);
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => new { i.UserId, i.InvoiceNumber }).IsUnique();
            builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(20);
            builder.Property(i => i.Subtotal).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Cgst).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Sgst).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Igst).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Total).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Status).HasConversion<string>();

            builder.HasOne(i => i.User)
             .WithMany(u => u.Invoices)
             .HasForeignKey(i => i.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Client)
             .WithMany(c => c.Invoices)
             .HasForeignKey(i => i.ClientId)
             .OnDelete(DeleteBehavior.NoAction); // avoid multiple cascade paths
        }
    }
}
