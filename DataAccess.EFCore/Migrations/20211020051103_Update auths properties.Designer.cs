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
    [Migration("20211020051103_Update auths properties")]
    partial class Updateauthsproperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataModels.Entities.Auth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Auths");
                });

            modelBuilder.Entity("DataModels.Entities.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Bik")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("BIK");

                    b.Property<string>("Inn")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("INN");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("P")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Insurance");
                });

            modelBuilder.Entity("DataModels.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .HasMaxLength(7)
                        .IsUnicode(false)
                        .HasColumnType("varchar(7)");

                    b.Property<byte[]>("DateTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DataModels.Entities.OrderService", b =>
                {
                    b.Property<bool?>("Accepted")
                        .HasColumnType("bit");

                    b.Property<string>("Analyzer")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<byte[]>("FinishedTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double?>("Result")
                        .HasColumnType("float");

                    b.Property<int>("ServiceCode")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasIndex("OrderId");

                    b.HasIndex("ServiceCode");

                    b.HasIndex("UserId");

                    b.ToTable("OrderService");
                });

            modelBuilder.Entity("DataModels.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("BirthdateTimestamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PassportN")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("Passport_n");

                    b.Property<string>("PassportS")
                        .IsRequired()
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("Passport_s");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DataModels.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DataModels.Entities.Safety", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IpAddress")
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("UA")
                        .HasColumnType("text")
                        .HasColumnName("UA");

                    b.HasKey("Id");

                    b.ToTable("Safety");
                });

            modelBuilder.Entity("DataModels.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("DataModels.Entities.Social", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EIN")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("EIN");

                    b.Property<string>("SocialSecNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SocialType")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.HasKey("Id");

                    b.ToTable("Social");
                });

            modelBuilder.Entity("DataModels.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ip")
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime?>("LastEnter")
                        .HasColumnType("date");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataModels.Entities.UserService", b =>
                {
                    b.Property<int>("CodeService")
                        .HasColumnType("int");

                    b.Property<int>("CodeUser")
                        .HasColumnType("int");

                    b.HasIndex("CodeService");

                    b.HasIndex("CodeUser");

                    b.ToTable("User-Services");
                });

            modelBuilder.Entity("DataModels.Entities.Order", b =>
                {
                    b.HasOne("DataModels.Entities.Patient", "Patient")
                        .WithMany("Orders")
                        .HasForeignKey("PatientId")
                        .HasConstraintName("FK_Order_Patients")
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DataModels.Entities.OrderService", b =>
                {
                    b.HasOne("DataModels.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Blood-Services_Blood")
                        .IsRequired();

                    b.HasOne("DataModels.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceCode")
                        .HasConstraintName("FK_Blood-Services_Services")
                        .IsRequired();

                    b.HasOne("DataModels.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Blood-Services_Users")
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataModels.Entities.Patient", b =>
                {
                    b.HasOne("DataModels.Entities.Insurance", "Insurance")
                        .WithOne("Patient")
                        .HasForeignKey("DataModels.Entities.Patient", "Id")
                        .HasConstraintName("FK_Patients_Insurance")
                        .IsRequired();

                    b.HasOne("DataModels.Entities.Safety", "Safety")
                        .WithOne("Patient")
                        .HasForeignKey("DataModels.Entities.Patient", "Id")
                        .HasConstraintName("FK_Patients_Safety")
                        .IsRequired();

                    b.HasOne("DataModels.Entities.Social", "Social")
                        .WithOne("Patient")
                        .HasForeignKey("DataModels.Entities.Patient", "Id")
                        .HasConstraintName("FK_Patients_Social")
                        .IsRequired();

                    b.Navigation("Insurance");

                    b.Navigation("Safety");

                    b.Navigation("Social");
                });

            modelBuilder.Entity("DataModels.Entities.User", b =>
                {
                    b.HasOne("DataModels.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Users_Roles")
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DataModels.Entities.UserService", b =>
                {
                    b.HasOne("DataModels.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("CodeService")
                        .HasConstraintName("FK_User-Services_Services")
                        .IsRequired();

                    b.HasOne("DataModels.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("CodeUser")
                        .HasConstraintName("FK_User-Services_Users")
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataModels.Entities.Insurance", b =>
                {
                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DataModels.Entities.Patient", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DataModels.Entities.Safety", b =>
                {
                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DataModels.Entities.Social", b =>
                {
                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
