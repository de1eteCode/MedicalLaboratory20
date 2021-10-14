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
                .HasKey(x => x.Id);

            builder
                .HasOne(p => p.Patient)
                .WithOne(p => p.Insurance)
                .HasForeignKey<Insurance>(p => p.Id);
        }
    }
}
