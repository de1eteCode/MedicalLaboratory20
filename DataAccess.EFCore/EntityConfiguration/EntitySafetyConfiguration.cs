using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntitySafetyConfiguration : IEntityTypeConfiguration<Safety>
    {
        public void Configure(EntityTypeBuilder<Safety> builder)
        {
            builder
                .ToTable("Safety");

            builder
                .Property(e => e.IpAddress)
                .HasMaxLength(16)
                .IsUnicode(false);

            builder
                .Property(e => e.UA)
                .HasColumnType("text")
                .HasColumnName("UA");
        }
    }
}
