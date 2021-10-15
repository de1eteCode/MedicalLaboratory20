using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.EntityConfiguration
{
    internal class EntityPatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.BirthdateTimestamp)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Fullname)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PassportN)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Passport_n");

            builder.Property(e => e.PassportS)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Passport_s");

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.HasOne(d => d.Insurance)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_Insurance");

            builder.HasOne(d => d.Safety)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_Safety");

            builder.HasOne(d => d.Social)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_Social");
        }
    }
}
