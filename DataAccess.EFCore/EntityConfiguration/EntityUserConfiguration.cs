using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntityUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.
                HasKey(x => x.Id);

            builder
                .HasMany(p => p.Services)
                .WithMany(p => p.Users)
                .UsingEntity(p => p.ToTable("UserServices"));
        }
    }
}
