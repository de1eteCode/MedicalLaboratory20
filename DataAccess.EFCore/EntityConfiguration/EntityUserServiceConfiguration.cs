using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntityUserServiceConfiguration : IEntityTypeConfiguration<UserService>
    {
        public void Configure(EntityTypeBuilder<UserService> builder)
        {
            builder
                .HasNoKey();

            builder
                .ToTable("User-Services");

            builder
                .HasOne(d => d.Service)
                .WithMany()
                .HasForeignKey(d => (object)d.CodeService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User-Services_Services");

            builder
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.CodeUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User-Services_Users");
        }
    }
}
