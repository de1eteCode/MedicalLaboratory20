using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntityUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(e => e.Ip)
                .HasMaxLength(16)
                .IsUnicode(false);

            builder
                .Property(e => e.LastEnter)
                .HasColumnType("date");

            builder
                .Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
