﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketTrackingSystem.DAL.Implementation;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    [DbContext(typeof(TicketTrackingSystemDbContext))]
    [Migration("20241225082208_addUsersToRole")]
    partial class addUsersToRole
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d2423e6f-bf14-4b76-9df3-46a9d068ceaf",
                            Email = "motaz@example.com",
                            EmailConfirmed = true,
                            FirstName = "Motaz",
                            LastName = "Allala",
                            LockoutEnabled = false,
                            NormalizedEmail = "MOTAZ@EXAMPLE.COM",
                            NormalizedUserName = "MOTAZALLALA",
                            PasswordHash = "AQAAAAIAAYagAAAAEBrrXYE9BDwLREHh41egZMXsAzi4a7F43Zsgo1GIs6er6GcsmaEFRR9Dnw1jZZX6Dg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b6f31767-b01e-4ef1-b5bf-1d6f43fb66f5",
                            TwoFactorEnabled = false,
                            UserName = "motazallala",
                            UserType = 0
                        },
                        new
                        {
                            Id = new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4e70a61a-f2fc-4e7c-9aad-8171f0193493",
                            Email = "sami@example.com",
                            EmailConfirmed = true,
                            FirstName = "Sami",
                            LastName = "Subarna",
                            LockoutEnabled = false,
                            NormalizedEmail = "SAMI@EXAMPLE.COM",
                            NormalizedUserName = "SAMISUBARNA",
                            PasswordHash = "AQAAAAIAAYagAAAAEAluhe52JefWzIpIehCV5PwrQaFWTJNenwzW0i9KAwiwrNJwmlaEcrclY+Ucq3R4bw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5a335be9-b270-4820-bd79-5606d465184a",
                            TwoFactorEnabled = false,
                            UserName = "samisubarna",
                            UserType = 0
                        },
                        new
                        {
                            Id = new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e7b92011-1ac9-4063-bd91-683d84f49269",
                            Email = "samiBA@example.com",
                            EmailConfirmed = true,
                            FirstName = "Sami",
                            LastName = "Subarna",
                            LockoutEnabled = false,
                            NormalizedEmail = "SAMIBA@EXAMPLE.COM",
                            NormalizedUserName = "SAMISUBARNABA",
                            PasswordHash = "AQAAAAIAAYagAAAAEKpDWWwWA3pfidwbLDfCi+3V9fOuC3ToBQOj67PeVRAjk3k75MhKBBRxgvUXlRSuOg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1bb60330-090f-4782-8b63-e8b417123642",
                            TwoFactorEnabled = false,
                            UserName = "samisubarnaBA",
                            UserType = 0
                        },
                        new
                        {
                            Id = new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ecb5ca91-90d3-45a4-b2d1-ad0b81099d6e",
                            Email = "samiDD@example.com",
                            EmailConfirmed = true,
                            FirstName = "Sami",
                            LastName = "Subarna",
                            LockoutEnabled = false,
                            NormalizedEmail = "SAMIDD@EXAMPLE.COM",
                            NormalizedUserName = "SAMISUBARNADD",
                            PasswordHash = "AQAAAAIAAYagAAAAEKPA7ulSMH9H8yYFA6+YZFUVjB1ICFWLf/T7w+Oki9Gaa6KbAz4C2GCMrJOzkeFjMQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "568ed90d-4832-401f-aa28-811c339c6f6e",
                            TwoFactorEnabled = false,
                            UserName = "samisubarnaDD",
                            UserType = 0
                        });
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Permission");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2f618f69-b884-4006-a916-8131f341f0b3"),
                            IsDeleted = false,
                            Name = "ViewProject"
                        },
                        new
                        {
                            Id = new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"),
                            IsDeleted = false,
                            Name = "EditProject",
                            ParentId = new Guid("2f618f69-b884-4006-a916-8131f341f0b3")
                        },
                        new
                        {
                            Id = new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"),
                            IsDeleted = false,
                            Name = "DeleteProject",
                            ParentId = new Guid("2f618f69-b884-4006-a916-8131f341f0b3")
                        },
                        new
                        {
                            Id = new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"),
                            IsDeleted = false,
                            Name = "CreateProject",
                            ParentId = new Guid("2f618f69-b884-4006-a916-8131f341f0b3")
                        },
                        new
                        {
                            Id = new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"),
                            IsDeleted = false,
                            Name = "ViewUser"
                        },
                        new
                        {
                            Id = new Guid("97f0e5b7-4895-43a5-b831-daf84374a752"),
                            IsDeleted = false,
                            Name = "EditUser",
                            ParentId = new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4")
                        },
                        new
                        {
                            Id = new Guid("e19eec9b-0c4b-4e8e-83cb-95e62a51c1b3"),
                            IsDeleted = false,
                            Name = "DeleteUser",
                            ParentId = new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4")
                        },
                        new
                        {
                            Id = new Guid("30a62382-8c8f-4a13-8806-88104f9f6066"),
                            IsDeleted = false,
                            Name = "CreateUser",
                            ParentId = new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4")
                        },
                        new
                        {
                            Id = new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7"),
                            IsDeleted = false,
                            Name = "ViewRole"
                        },
                        new
                        {
                            Id = new Guid("ac132e0d-4d87-4217-a648-b8e97c5f4d6f"),
                            IsDeleted = false,
                            Name = "EditRole",
                            ParentId = new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7")
                        },
                        new
                        {
                            Id = new Guid("88b6dda7-cfce-4cc5-833d-2f8cf8fea5c4"),
                            IsDeleted = false,
                            Name = "DeleteRole",
                            ParentId = new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7")
                        },
                        new
                        {
                            Id = new Guid("6d370d45-c2f0-4691-a80e-91e1de7f9c1c"),
                            IsDeleted = false,
                            Name = "CreateRole",
                            ParentId = new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7")
                        },
                        new
                        {
                            Id = new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb"),
                            IsDeleted = false,
                            Name = "ViewPermission"
                        },
                        new
                        {
                            Id = new Guid("4b346e07-abbb-4f62-a3f9-4c109bc153f2"),
                            IsDeleted = false,
                            Name = "EditPermission",
                            ParentId = new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb")
                        },
                        new
                        {
                            Id = new Guid("d634bd07-26c3-421d-8fb7-34747a258af7"),
                            IsDeleted = false,
                            Name = "DeletePermission",
                            ParentId = new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb")
                        },
                        new
                        {
                            Id = new Guid("5b28fb44-8e97-4a71-b5a2-b041cf58b7d2"),
                            IsDeleted = false,
                            Name = "CreatePermission",
                            ParentId = new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb")
                        },
                        new
                        {
                            Id = new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8"),
                            IsDeleted = false,
                            Name = "ViewDepartment"
                        },
                        new
                        {
                            Id = new Guid("5e4fdcac-48ff-42fe-b06e-912dbdf73d60"),
                            IsDeleted = false,
                            Name = "EditDepartment",
                            ParentId = new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8")
                        },
                        new
                        {
                            Id = new Guid("f8dde594-4b1b-4a0a-9495-9818a0636de2"),
                            IsDeleted = false,
                            Name = "DeleteDepartment",
                            ParentId = new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8")
                        },
                        new
                        {
                            Id = new Guid("77b7c29f-6b7b-4fc5-937b-37e8aa0e37f4"),
                            IsDeleted = false,
                            Name = "CreateDepartment",
                            ParentId = new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8")
                        },
                        new
                        {
                            Id = new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9"),
                            IsDeleted = false,
                            Name = "ViewClient"
                        },
                        new
                        {
                            Id = new Guid("a2282ae2-16b1-43b5-93bd-5f75045e1919"),
                            IsDeleted = false,
                            Name = "EditClient",
                            ParentId = new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9")
                        },
                        new
                        {
                            Id = new Guid("e79d0c94-7875-4c1e-a93c-77f1fcdb303a"),
                            IsDeleted = false,
                            Name = "DeleteClient",
                            ParentId = new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9")
                        },
                        new
                        {
                            Id = new Guid("1e4a95a1-c2db-46b4-a2cc-445da1e76a8c"),
                            IsDeleted = false,
                            Name = "CreateClient",
                            ParentId = new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9")
                        },
                        new
                        {
                            Id = new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a"),
                            IsDeleted = false,
                            Name = "ViewTicket"
                        },
                        new
                        {
                            Id = new Guid("94ebcab5-660b-4e8d-80c1-8ad58cb7f2c5"),
                            IsDeleted = false,
                            Name = "EditTicket",
                            ParentId = new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a")
                        },
                        new
                        {
                            Id = new Guid("a4994d1d-091f-44a9-9df8-793f23634a9b"),
                            IsDeleted = false,
                            Name = "DeleteTicket",
                            ParentId = new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a")
                        },
                        new
                        {
                            Id = new Guid("1a12f6f4-88ff-4ab6-9e44-013cbf5c1964"),
                            IsDeleted = false,
                            Name = "CreateTicket",
                            ParentId = new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a")
                        });
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.ProjectMember", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Stage")
                        .HasColumnType("int");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectMember");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"),
                            Name = "BusinessAnalyses",
                            NormalizedName = "BUSINESSANAlYSES"
                        },
                        new
                        {
                            Id = new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"),
                            Name = "DevelopmentDepartment",
                            NormalizedName = "DEVElOPMENTDEPARTMENT"
                        },
                        new
                        {
                            Id = new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"),
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.RolePermission", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                            PermissionId = new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4")
                        },
                        new
                        {
                            RoleId = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                            PermissionId = new Guid("97f0e5b7-4895-43a5-b831-daf84374a752")
                        },
                        new
                        {
                            RoleId = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                            PermissionId = new Guid("e19eec9b-0c4b-4e8e-83cb-95e62a51c1b3")
                        },
                        new
                        {
                            RoleId = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                            PermissionId = new Guid("30a62382-8c8f-4a13-8806-88104f9f6066")
                        },
                        new
                        {
                            RoleId = new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"),
                            PermissionId = new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4")
                        },
                        new
                        {
                            RoleId = new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"),
                            PermissionId = new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb")
                        });
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Stage")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                            RoleId = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57")
                        },
                        new
                        {
                            UserId = new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                            RoleId = new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4")
                        },
                        new
                        {
                            UserId = new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                            RoleId = new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f")
                        },
                        new
                        {
                            UserId = new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                            RoleId = new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.ApplicationUser", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Permission", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.Permission", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.ProjectMember", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.Project", "Project")
                        .WithMany("Members")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTrackingSystem.Core.Model.ApplicationUser", "User")
                        .WithMany("ProjectMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.RolePermission", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTrackingSystem.Core.Model.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Ticket", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.ApplicationUser", "Creator")
                        .WithMany("Tickets")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTrackingSystem.Core.Model.Project", "Project")
                        .WithMany("Tickets")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.UserRole", b =>
                {
                    b.HasOne("TicketTrackingSystem.Core.Model.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketTrackingSystem.Core.Model.ApplicationUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.ApplicationUser", b =>
                {
                    b.Navigation("ProjectMembers");

                    b.Navigation("Roles");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Permission", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Project", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketTrackingSystem.Core.Model.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
