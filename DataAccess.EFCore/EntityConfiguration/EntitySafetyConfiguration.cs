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
                .HasKey(x => x.Id);

            builder
                .HasOne(p => p.Patient)
                .WithOne(p => p.Safety)
                .HasForeignKey<Safety>(p => p.Id);
        }
    }
}
