﻿// <auto-generated />
using System;
using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.EFCore.Migrations
{
    [DbContext(typeof(LaboratoryContext))]
    [Migration("20211014183832_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataModels.Entities.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BIK")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("INN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("DataModels.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataModels.Entities.OrderService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<string>("Analyzer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinishedTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<float>("Result")
                        .HasColumnType("real");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderServices");
                });

            modelBuilder.Entity("DataModels.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrithdateTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coutry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passport_n")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passport_s")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DataModels.Entities.Safety", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UA")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Safeties");
                });

            modelBuilder.Entity("DataModels.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("DataModels.Entities.Social", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("EIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialSecNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Socials");
                });

            modelBuilder.Entity("DataModels.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastEnter")
                        .HasColumnType("datetime2");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ServiceUser", b =>
                {
                    b.Property<int>("ServicesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("ServicesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserServices");
                });

            modelBuilder.Entity("DataModels.Entities.Insurance", b =>
                {
                    b.HasOne("DataModels.Entities.Patient", "Patient")
                        .WithOne("Insurance")
                        .HasForeignKey("DataModels.Entities.Insurance", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DataModels.Entities.Order", b =>
                {
                    b.HasOne("DataModels.Entities.Patient", "Patient")
                        .WithMany("Orders")
                        .HasForeignKey("PatientId");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DataModels.Entities.OrderService", b =>
                {
                    b.HasOne("DataModels.Entities.Order", "Order")
                        .WithMany("OrderServices")
                        .HasForeignKey("OrderId");

                    b.HasOne("DataModels.Entities.Service", "Service")
                        .WithMany("OrderServices")
                        .HasForeignKey("ServiceId");

                    b.HasOne("DataModels.Entities.User", "User")
                        .WithMany("OrderServices")
                        .HasForeignKey("UserId");

                    b.Navigation("Order");

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataModels.Entities.Safety", b =>
                {
                    b.HasOne("DataModels.Entities.Patient", "Patient")
                        .WithOne("Safety")
                        .HasForeignKey("DataModels.Entities.Safety", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DataModels.Entities.Social", b =>
                {
                    b.HasOne("DataModels.Entities.Patient", "Patient")
                        .WithOne("Social")
                        .HasForeignKey("DataModels.Entities.Social", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ServiceUser", b =>
                {
                    b.HasOne("DataModels.Entities.Service", null)
                        .WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModels.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModels.Entities.Order", b =>
                {
                    b.Navigation("OrderServices");
                });

            modelBuilder.Entity("DataModels.Entities.Patient", b =>
                {
                    b.Navigation("Insurance");

                    b.Navigation("Orders");

                    b.Navigation("Safety");

                    b.Navigation("Social");
                });

            modelBuilder.Entity("DataModels.Entities.Service", b =>
                {
                    b.Navigation("OrderServices");
                });

            modelBuilder.Entity("DataModels.Entities.User", b =>
                {
                    b.Navigation("OrderServices");
                });
#pragma warning restore 612, 618
        }
    }
}
