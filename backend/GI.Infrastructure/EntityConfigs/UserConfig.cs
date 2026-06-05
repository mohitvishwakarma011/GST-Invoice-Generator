using GI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GI.Infrastructure.EntityConfigs
{
    internal class UserConfig : BaseConfig<User>
    {   
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(x => x.PasswordHash)
                   .IsRequired();

            builder.Property(x => x.BusinessName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Gstin)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(x => x.Address)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(x => x.State)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.StateCode)
                .IsRequired();

            builder.Property(x => x.BankName)
                   .HasMaxLength(100);

            builder.Property(x => x.AccountNumber)
                   .HasMaxLength(30);

            builder.Property(x => x.IfscCode)
                   .HasMaxLength(20);

            builder.Property(x => x.UpiId)
                   .HasMaxLength(100);

            builder.HasIndex(x => x.Email)
                   .IsUnique();

            builder.HasIndex(x => x.Gstin)
                   .IsUnique();
        }
    }
}
