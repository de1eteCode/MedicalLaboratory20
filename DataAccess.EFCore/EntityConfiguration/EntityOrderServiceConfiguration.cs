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
                .HasKey(x =>  x.Id);

            builder
                .HasOne<Service>(p => p.Service)
                .WithMany(p => p.OrderServices)
                .HasForeignKey("ServiceId");

            builder
                .HasOne<User>(p => p.User)
                .WithMany(p => p.OrderServices)
                .HasForeignKey("UserId");

            builder
                .HasOne<Order>(p => p.Order)
                .WithMany(p => p.OrderServices)
                .HasForeignKey("OrderId");
        }
    }
}
