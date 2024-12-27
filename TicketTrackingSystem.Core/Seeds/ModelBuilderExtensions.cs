using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.Core.Seeds;
public static class ModelBuilderExtensions
{
    public static void SeedUsers(this ModelBuilder builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.Entity<ApplicationUser>().HasData(
             new ApplicationUser
             {
                 Id = Guid.Parse("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"), // Static GUID for user1
                 UserName = "motazallala",
                 NormalizedUserName = "MOTAZALLALA",  // Correct normalization
                 Email = "motaz@example.com",
                 NormalizedEmail = "MOTAZ@EXAMPLE.COM",  // Correct normalization
                 EmailConfirmed = true,
                 FirstName = "Motaz",
                 LastName = "Allala",
                 SecurityStamp = Guid.NewGuid().ToString(),
                 PasswordHash = hasher.HashPassword(null, "123@qwE")
             },
            new ApplicationUser
            {
                Id = Guid.Parse("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"), // Static GUID for user2
                UserName = "samisubarna",
                NormalizedUserName = "SAMISUBARNA",  // Correct normalization
                Email = "sami@example.com",
                NormalizedEmail = "SAMI@EXAMPLE.COM",  // Correct normalization
                EmailConfirmed = true,
                FirstName = "Sami",
                LastName = "Subarna",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "123@qwE")
            },
            new ApplicationUser
            {
                Id = Guid.Parse("9229F7AA-5B2F-4B72-BDB3-6F786A0C62BE"), // Static GUID for user3
                UserName = "samisubarnaBA",
                NormalizedUserName = "SAMISUBARNABA",  // Correct normalization
                Email = "samiBA@example.com",
                NormalizedEmail = "SAMIBA@EXAMPLE.COM",  // Correct normalization
                EmailConfirmed = true,
                FirstName = "Sami",
                LastName = "Subarna",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "123@qwE")
            },
            new ApplicationUser
            {
                Id = Guid.Parse("73A03B91-0B28-4838-9D48-C30B7ACE75A0"), // Static GUID for user4
                UserName = "samisubarnaDD",
                NormalizedUserName = "SAMISUBARNADD",  // Correct normalization
                Email = "samiDD@example.com",
                NormalizedEmail = "SAMIDD@EXAMPLE.COM",  // Correct normalization
                EmailConfirmed = true,
                FirstName = "Sami",
                LastName = "Subarna",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "123@qwE")
            }
        );
    }
    public static void SeedRoles(this ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(
            new Role
            {
                Id = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // Static GUID for Admin role
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new Role
            {
                Id = Guid.Parse("E143EF8A-95C2-4359-B1B6-7FDE456B771F"),
                Name = "BusinessAnalyses",
                NormalizedName = "BUSINESSANAlYSES"
            },
            new Role
            {
                Id = Guid.Parse("DEB2A077-7A07-49F4-BDDA-3C7F95061D72"),
                Name = "DevelopmentDepartment",
                NormalizedName = "DEVElOPMENTDEPARTMENT"
            },
            new Role
            {
                Id = Guid.Parse("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), // Static GUID for User role
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }


    public static void SeedUserRoles(this ModelBuilder builder)
    {
        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = Guid.Parse("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"), // GUID for motazallala
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") // GUID for Admin role
            },
            new UserRole
            {
                UserId = Guid.Parse("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"), // GUID for samisubarna
                RoleId = Guid.Parse("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4")  // GUID for User role
            },
            new UserRole
            {
                UserId = Guid.Parse("9229F7AA-5B2F-4B72-BDB3-6F786A0C62BE"), // Static GUID for user3
                RoleId = Guid.Parse("E143EF8A-95C2-4359-B1B6-7FDE456B771F"),// GUID for BusinessAnalyses role
            },
            new UserRole
            {
                UserId = Guid.Parse("73A03B91-0B28-4838-9D48-C30B7ACE75A0"), // Static GUID for user4
                RoleId = Guid.Parse("DEB2A077-7A07-49F4-BDDA-3C7F95061D72")// GUID for DevelopmentDepartment role
            }

        );
    }

    public static void SeedPermissions(this ModelBuilder builder)
    {
        builder.Entity<Permission>().HasData(
            // Permissions for the Project page
            new Permission
            {
                Id = Guid.Parse("2F618F69-B884-4006-A916-8131F341F0B3"),
                Name = PermissionName.ViewProject.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("80418A92-7E17-4B3D-880A-42BF0DF503CB"),
                Name = PermissionName.EditProject.ToString(),
                ParentId = Guid.Parse("2F618F69-B884-4006-A916-8131F341F0B3")
            },
            new Permission
            {
                Id = Guid.Parse("2702C229-4DF3-4D01-99DF-DB962CB93C3D"),
                Name = PermissionName.DeleteProject.ToString(),
                ParentId = Guid.Parse("2F618F69-B884-4006-A916-8131F341F0B3")
            },
            new Permission
            {
                Id = Guid.Parse("F4CED4D6-6844-483D-8B18-A0941A5C266E"),
                Name = PermissionName.CreateProject.ToString(),
                ParentId = Guid.Parse("2F618F69-B884-4006-A916-8131F341F0B3")
            },

            // Permissions for the User page
            new Permission
            {
                Id = Guid.Parse("D19358C8-5CE7-4C13-8B37-EBFD676E8EB4"),
                Name = PermissionName.ViewUser.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("97F0E5B7-4895-43A5-B831-DAF84374A752"),
                Name = PermissionName.EditUser.ToString(),
                ParentId = Guid.Parse("D19358C8-5CE7-4C13-8B37-EBFD676E8EB4")
            },
            new Permission
            {
                Id = Guid.Parse("E19EEC9B-0C4B-4E8E-83CB-95E62A51C1B3"),
                Name = PermissionName.DeleteUser.ToString(),
                ParentId = Guid.Parse("D19358C8-5CE7-4C13-8B37-EBFD676E8EB4")
            },
            new Permission
            {
                Id = Guid.Parse("30A62382-8C8F-4A13-8806-88104F9F6066"),
                Name = PermissionName.CreateUser.ToString(),
                ParentId = Guid.Parse("D19358C8-5CE7-4C13-8B37-EBFD676E8EB4")
            },

            // Permissions for the Role page
            new Permission
            {
                Id = Guid.Parse("EA4085FE-BCC9-44AC-B3F6-3D3F1055B6A7"),
                Name = PermissionName.ViewRole.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("AC132E0D-4D87-4217-A648-B8E97C5F4D6F"),
                Name = PermissionName.EditRole.ToString(),
                ParentId = Guid.Parse("EA4085FE-BCC9-44AC-B3F6-3D3F1055B6A7")
            },
            new Permission
            {
                Id = Guid.Parse("88B6DDA7-CFCE-4CC5-833D-2F8CF8FEA5C4"),
                Name = PermissionName.DeleteRole.ToString(),
                ParentId = Guid.Parse("EA4085FE-BCC9-44AC-B3F6-3D3F1055B6A7")
            },
            new Permission
            {
                Id = Guid.Parse("6D370D45-C2F0-4691-A80E-91E1DE7F9C1C"),
                Name = PermissionName.CreateRole.ToString(),
                ParentId = Guid.Parse("EA4085FE-BCC9-44AC-B3F6-3D3F1055B6A7"),
            },

            // Permissions for the Permission page
            new Permission
            {
                Id = Guid.Parse("F595DBE8-8E15-4D3F-8A37-85A5E710FBBB"),
                Name = PermissionName.ViewPermission.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("4B346E07-ABBB-4F62-A3F9-4C109BC153F2"),
                Name = PermissionName.EditPermission.ToString(),
                ParentId = Guid.Parse("F595DBE8-8E15-4D3F-8A37-85A5E710FBBB")
            },
            new Permission
            {
                Id = Guid.Parse("D634BD07-26C3-421D-8FB7-34747A258AF7"),
                Name = PermissionName.DeletePermission.ToString(),
                ParentId = Guid.Parse("F595DBE8-8E15-4D3F-8A37-85A5E710FBBB")
            },
            new Permission
            {
                Id = Guid.Parse("5B28FB44-8E97-4A71-B5A2-B041CF58B7D2"),
                Name = PermissionName.CreatePermission.ToString(),
                ParentId = Guid.Parse("F595DBE8-8E15-4D3F-8A37-85A5E710FBBB")
            },

            // Permissions for the Department page
            new Permission
            {
                Id = Guid.Parse("773F9A24-48EB-46CF-B6CB-C0B607A85BC8"),
                Name = PermissionName.ViewDepartment.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("5E4FDCAC-48FF-42FE-B06E-912DBDF73D60"),
                Name = PermissionName.EditDepartment.ToString(),
                ParentId = Guid.Parse("773F9A24-48EB-46CF-B6CB-C0B607A85BC8")
            },
            new Permission
            {
                Id = Guid.Parse("F8DDE594-4B1B-4A0A-9495-9818A0636DE2"),
                Name = PermissionName.DeleteDepartment.ToString(),
                ParentId = Guid.Parse("773F9A24-48EB-46CF-B6CB-C0B607A85BC8")
            },
            new Permission
            {
                Id = Guid.Parse("77B7C29F-6B7B-4FC5-937B-37E8AA0E37F4"),
                Name = PermissionName.CreateDepartment.ToString(),
                ParentId = Guid.Parse("773F9A24-48EB-46CF-B6CB-C0B607A85BC8")
            },

            // Permissions for the Client page
            new Permission
            {
                Id = Guid.Parse("CA4EAB94-88D5-4FD9-A4A2-5EAF8AD927C9"),
                Name = PermissionName.ViewClient.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("A2282AE2-16B1-43B5-93BD-5F75045E1919"),
                Name = PermissionName.EditClient.ToString(),
                ParentId = Guid.Parse("CA4EAB94-88D5-4FD9-A4A2-5EAF8AD927C9")
            },
            new Permission
            {
                Id = Guid.Parse("E79D0C94-7875-4C1E-A93C-77F1FCDB303A"),
                Name = PermissionName.DeleteClient.ToString(),
                ParentId = Guid.Parse("CA4EAB94-88D5-4FD9-A4A2-5EAF8AD927C9")
            },
            new Permission
            {
                Id = Guid.Parse("1E4A95A1-C2DB-46B4-A2CC-445DA1E76A8C"),
                Name = PermissionName.CreateClient.ToString(),
                ParentId = Guid.Parse("CA4EAB94-88D5-4FD9-A4A2-5EAF8AD927C9")
            },

            // Permissions for the Ticket page
            new Permission
            {
                Id = Guid.Parse("B6F8C089-740E-4741-8456-6F8D99E9657A"),
                Name = PermissionName.ViewTicket.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("94EBCAB5-660B-4E8D-80C1-8AD58CB7F2C5"),
                Name = PermissionName.EditTicket.ToString(),
                ParentId = Guid.Parse("B6F8C089-740E-4741-8456-6F8D99E9657A")
            },
            new Permission
            {
                Id = Guid.Parse("A4994D1D-091F-44A9-9DF8-793F23634A9B"),
                Name = PermissionName.DeleteTicket.ToString(),
                ParentId = Guid.Parse("B6F8C089-740E-4741-8456-6F8D99E9657A")
            },
            new Permission
            {
                Id = Guid.Parse("1A12F6F4-88FF-4AB6-9E44-013CBF5C1964"),
                Name = PermissionName.CreateTicket.ToString(),
                ParentId = Guid.Parse("B6F8C089-740E-4741-8456-6F8D99E9657A")
            }
        );


    }
    public static void SeedRolePermissions(this ModelBuilder builder)
    {
        builder.Entity<RolePermission>().HasData(
            // Admin role permissions
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("D19358C8-5CE7-4C13-8B37-EBFD676E8EB4"), // GUID for ViewUser permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("97F0E5B7-4895-43A5-B831-DAF84374A752"), // GUID for EditUser permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("E19EEC9B-0C4B-4E8E-83CB-95E62A51C1B3"), // GUID for DeleteUser permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("30A62382-8C8F-4A13-8806-88104F9F6066"), // GUID for CreateUser permission
            },

            // BusinessAnalyses role permissions
            new RolePermission
            {
                RoleId = Guid.Parse("E143EF8A-95C2-4359-B1B6-7FDE456B771F"), // GUID for BusinessAnalyses role
                PermissionId = Guid.Parse("D19358C8-5CE7-4C13-8B37-EBFD676E8EB4"), // GUID for ViewUser permission
            },

            // DevelopmentDepartment role permissions

            // User role permissions
            new RolePermission
            {
                RoleId = Guid.Parse("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), // GUID for User role
                PermissionId = Guid.Parse("80418A92-7E17-4B3D-880A-42BF0DF503CB"), // GUID for EditProject permission
            }
        );
    }
}