using GI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GI.Infrastructure.EntityConfigs
{
    internal class InvoiceItemConfig : BaseConfig<InvoiceItem>
    {
        public override void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            base.Configure(builder);
            builder.HasKey(ii => ii.Id);
            builder.Property(ii => ii.Description).IsRequired().HasMaxLength(500);
            builder.Property(ii => ii.HsnCode).HasMaxLength(10);
            builder.Property(ii => ii.Quantity).HasColumnType("decimal(18,2)");
            builder.Property(ii => ii.Rate).HasColumnType("decimal(18,2)");
            builder.Property(ii => ii.Amount).HasColumnType("decimal(18,2)");

            builder.HasOne(ii => ii.Invoice)
             .WithMany(i => i.Items)
             .HasForeignKey(ii => ii.InvoiceId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
