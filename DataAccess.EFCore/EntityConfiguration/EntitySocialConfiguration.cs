using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntitySocialConfiguration : IEntityTypeConfiguration<Social>
    {
        public void Configure(EntityTypeBuilder<Social> builder)
        {
            builder
                .ToTable("Social");

            builder
                .Property(e => e.EIN)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EIN");

            builder
                .Property(e => e.SocialSecNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder
                .Property(e => e.SocialType)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false);
        }
    }
}
