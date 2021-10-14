using DataAccess.EFCore.EntityConfiguration;
using DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore
{
    public class LaboratoryContext : DbContext
    {
        public LaboratoryContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new EntityInsuranceConfiguration());
            //modelBuilder.ApplyConfiguration(new EntityOrderConfiguration());
            //modelBuilder.ApplyConfiguration(new EntityOrderServiceConfiguration());
            //modelBuilder.ApplyConfiguration(new EntityPatientConfiguration());
            //modelBuilder.ApplyConfiguration(new EntitySafetyConfiguration());
            //modelBuilder.ApplyConfiguration(new EntitySocialConfiguration());
            //modelBuilder.ApplyConfiguration(new EntityUserConfiguration());
        }

        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderService> OrderServices { get; set;}
        public DbSet<Patient> Patients {  get; set; }
        public DbSet<Safety> Safeties { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<User> Users { get; set; }
    }
}