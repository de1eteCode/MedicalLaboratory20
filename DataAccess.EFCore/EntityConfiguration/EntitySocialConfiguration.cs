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
                .HasKey(x => x.Id);

            builder
                .HasOne(p => p.Patient)
                .WithOne(p => p.Social)
                .HasForeignKey<Social>(p => p.Id);
        }
    }
}
