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
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.ApplyConfiguration(new EntityInsuranceConfiguration());
            modelBuilder.ApplyConfiguration(new EntityOrderConfiguration());
            modelBuilder.ApplyConfiguration(new EntityOrderServiceConfiguration());
            modelBuilder.ApplyConfiguration(new EntityPatientConfiguration());
            modelBuilder.ApplyConfiguration(new EntitySafetyConfiguration());
            modelBuilder.ApplyConfiguration(new EntitySocialConfiguration());
            modelBuilder.ApplyConfiguration(new EntityUserConfiguration());
            modelBuilder.ApplyConfiguration(new EntityUserServiceConfiguration());
            modelBuilder.ApplyConfiguration(new EntityRoleConfiguration());
        }

        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderService> OrderServices { get; set;}
        public virtual DbSet<Patient> Patients {  get; set; }
        public virtual DbSet<Safety> Safeties { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Social> Socials { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserService> UserServices { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}