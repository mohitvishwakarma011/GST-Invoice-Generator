using Microsoft.EntityFrameworkCore;
using GI.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GI.Infrastructure.EntityConfigs
{
    internal class BaseConfig<T> : IEntityTypeConfiguration<T> where T : BaseAudit
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedBy)
                .IsRequired();
            builder.Property(x => x.CreatedOn)
                .IsRequired();
            builder.Property(x => x.UpdatedOn)
                .IsRequired(false);
        }
    }
}
