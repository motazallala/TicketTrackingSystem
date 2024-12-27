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
    [Migration("20241224140406_changeLogicOfPermission")]
    partial class changeLogicOfPermission
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
                            ConcurrencyStamp = "2c0e54bc-7315-4933-bb8f-e2750db70fa4",
                            Email = "motaz@example.com",
                            EmailConfirmed = true,
                            FirstName = "Motaz",
                            LastName = "Allala",
                            LockoutEnabled = false,
                            NormalizedEmail = "MOTAZ@EXAMPLE.COM",
                            NormalizedUserName = "MOTAZALLALA",
                            PasswordHash = "AQAAAAIAAYagAAAAEBJ+On+VWb4ZyMDWOI/urQYSNGIai0qmWv32/gbk4PkC1RQNJjVXC42Ko1wOfLo/Nw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "6dd044a5-3e2a-43f9-9de2-c9b487f73d24",
                            TwoFactorEnabled = false,
                            UserName = "motazallala",
                            UserType = 0
                        },
                        new
                        {
                            Id = new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d5b68c12-cb83-4115-a430-19ed4969cc46",
                            Email = "sami@example.com",
                            EmailConfirmed = true,
                            FirstName = "Sami",
                            LastName = "Subarna",
                            LockoutEnabled = false,
                            NormalizedEmail = "SAMI@EXAMPLE.COM",
                            NormalizedUserName = "SAMISUBARNA",
                            PasswordHash = "AQAAAAIAAYagAAAAEDWkt0OqfUgGHB3SDYsg6+ZvOgQN7mgMw6KgKIKpOZrwBE7J6Ml5qjRspyfqLVwj/w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b0415ed6-d4ff-4ecb-b777-f38b7059c61a",
                            TwoFactorEnabled = false,
                            UserName = "samisubarna",
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
                            Name = "View"
                        },
                        new
                        {
                            Id = new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"),
                            IsDeleted = false,
                            Name = "Edit"
                        },
                        new
                        {
                            Id = new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"),
                            IsDeleted = false,
                            Name = "Delete"
                        },
                        new
                        {
                            Id = new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"),
                            IsDeleted = false,
                            Name = "Create"
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
                            PermissionId = new Guid("2f618f69-b884-4006-a916-8131f341f0b3")
                        },
                        new
                        {
                            RoleId = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                            PermissionId = new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb")
                        },
                        new
                        {
                            RoleId = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                            PermissionId = new Guid("2702c229-4df3-4d01-99df-db962cb93c3d")
                        },
                        new
                        {
                            RoleId = new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                            PermissionId = new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e")
                        },
                        new
                        {
                            RoleId = new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"),
                            PermissionId = new Guid("2f618f69-b884-4006-a916-8131f341f0b3")
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
