using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntityRoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .Property(e => e.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

        }
    }
}
