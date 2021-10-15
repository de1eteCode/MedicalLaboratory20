using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntityOrderServiceConfiguration : IEntityTypeConfiguration<OrderService>
    {
        public void Configure(EntityTypeBuilder<OrderService> builder)
        {
            builder
                .HasNoKey();

            builder
                .ToTable("OrderService");

            builder
                .Property(e => e.Analyzer)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder
                .Property(e => e.FinishedTimestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder
                .Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder
                .HasOne(d => d.Order)
                .WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blood-Services_Blood");

            builder
                .HasOne(d => d.Service)
                .WithMany()
                .HasForeignKey(d => d.ServiceCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blood-Services_Services");

            builder
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blood-Services_Users");
        }
    }
}
