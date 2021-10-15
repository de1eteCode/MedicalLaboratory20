using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntityOrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .ToTable("Order");

            builder
                .Property(e => e.Barcode)
                .HasMaxLength(7)
                .IsUnicode(false);

            builder
                .Property(e => e.DateTimestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder
                .HasOne(d => d.Patient)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Patients");
        }
    }
}
