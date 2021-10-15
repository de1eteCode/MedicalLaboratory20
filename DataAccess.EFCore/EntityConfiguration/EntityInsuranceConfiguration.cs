using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntityInsuranceConfiguration : IEntityTypeConfiguration<Insurance>
    {
        public void Configure(EntityTypeBuilder<Insurance> builder)
        {
            builder
                .ToTable(nameof(Insurance));

            builder
                .Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder
                .Property(e => e.Bik)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BIK");

            builder.Property(e => e.Inn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("INN");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.P)
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}
