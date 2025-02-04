﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.Core.Model.Enum;

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
                 UserType = UserType.Member,
                 LastName = "Allala",
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
            },
            // Permissions for the Ticket Message page
            new Permission
            {
                Id = Guid.Parse("3E353A51-E46D-446C-A385-38D8F6DDEB79"),
                Name = PermissionName.ViewTicketMessage.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("607AA269-DA9E-498C-9CE6-9CF380583542"),
                Name = PermissionName.EditTicketMessage.ToString(),
                ParentId = Guid.Parse("3E353A51-E46D-446C-A385-38D8F6DDEB79")
            },
            new Permission
            {
                Id = Guid.Parse("4C93EC77-B8A5-4B58-AA26-1D854E81B666"),
                Name = PermissionName.DeleteTicketMessage.ToString(),
                ParentId = Guid.Parse("3E353A51-E46D-446C-A385-38D8F6DDEB79")
            },
            new Permission
            {
                Id = Guid.Parse("AD7C90EB-083D-4A09-B99E-6BD25E30C5B3"),
                Name = PermissionName.CreateTicketMessage.ToString(),
                ParentId = Guid.Parse("3E353A51-E46D-446C-A385-38D8F6DDEB79")
            },

            // Permissions for the Ticket History page
            new Permission
            {
                Id = Guid.Parse("0334A2FC-4DC3-48AF-BF24-32F35CC192D8"),
                Name = PermissionName.ViewTicketHistory.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("15699509-04EB-40E5-A5D4-5EFFE8F339DD"),
                Name = PermissionName.EditTicketHistory.ToString(),
                ParentId = Guid.Parse("0334A2FC-4DC3-48AF-BF24-32F35CC192D8")
            },
            new Permission
            {
                Id = Guid.Parse("7B70FD1B-58E4-450A-B0CF-B99C040AC9A4"),
                Name = PermissionName.DeleteTicketHistory.ToString(),
                ParentId = Guid.Parse("0334A2FC-4DC3-48AF-BF24-32F35CC192D8")
            },
            new Permission
            {
                Id = Guid.Parse("EC43EEFF-88C2-4C22-AD71-71BA7F2CDA44"),
                Name = PermissionName.CreateTicketHistory.ToString(),
                ParentId = Guid.Parse("0334A2FC-4DC3-48AF-BF24-32F35CC192D8")
            },

            // Permissions for the Ticket Report page
            new Permission
            {
                Id = Guid.Parse("200A2920-D8FD-4D10-9FF9-30A6156A5879"),
                Name = PermissionName.ViewTicketReport.ToString()
            },
            new Permission
            {
                Id = Guid.Parse("8DC4DDC3-C1E4-4E32-A1E0-508BAA89B3DB"),
                Name = PermissionName.EditTicketReport.ToString(),
                ParentId = Guid.Parse("200A2920-D8FD-4D10-9FF9-30A6156A5879")
            },
            new Permission
            {
                Id = Guid.Parse("6170D82B-9E6F-41DB-9BCF-C4C265BF0F37"),
                Name = PermissionName.DeleteTicketReport.ToString(),
                ParentId = Guid.Parse("200A2920-D8FD-4D10-9FF9-30A6156A5879")
            },
            new Permission
            {
                Id = Guid.Parse("6D774C98-7BD9-40A4-A735-2C433C1F2D03"),
                Name = PermissionName.CreateTicketReport.ToString(),
                ParentId = Guid.Parse("200A2920-D8FD-4D10-9FF9-30A6156A5879")
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
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("2F618F69-B884-4006-A916-8131F341F0B3"), // GUID for ViewProject permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("80418A92-7E17-4B3D-880A-42BF0DF503CB"), // GUID for EditProject permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("2702C229-4DF3-4D01-99DF-DB962CB93C3D"), // GUID for DeleteProject permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("F4CED4D6-6844-483D-8B18-A0941A5C266E"), // GUID for CreateProject permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("EA4085FE-BCC9-44AC-B3F6-3D3F1055B6A7"), // GUID for ViewRole permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("AC132E0D-4D87-4217-A648-B8E97C5F4D6F"), // GUID for EditRole permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("88B6DDA7-CFCE-4CC5-833D-2F8CF8FEA5C4"), // GUID for DeleteRole permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("6D370D45-C2F0-4691-A80E-91E1DE7F9C1C"), // GUID for CreateRole permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("F595DBE8-8E15-4D3F-8A37-85A5E710FBBB"), // GUID for ViewPermission permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("4B346E07-ABBB-4F62-A3F9-4C109BC153F2"), // GUID for EditPermission permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("D634BD07-26C3-421D-8FB7-34747A258AF7"), // GUID for DeletePermission permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("5B28FB44-8E97-4A71-B5A2-B041CF58B7D2"), // GUID for CreatePermission permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("773F9A24-48EB-46CF-B6CB-C0B607A85BC8"), // GUID for ViewDepartment permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("5E4FDCAC-48FF-42FE-B06E-912DBDF73D60"), // GUID for EditDepartment permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("F8DDE594-4B1B-4A0A-9495-9818A0636DE2"), // GUID for DeleteDepartment permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("77B7C29F-6B7B-4FC5-937B-37E8AA0E37F4"), // GUID for CreateDepartment permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("CA4EAB94-88D5-4FD9-A4A2-5EAF8AD927C9"), // GUID for ViewClient permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("A2282AE2-16B1-43B5-93BD-5F75045E1919"), // GUID for EditClient permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("E79D0C94-7875-4C1E-A93C-77F1FCDB303A"), // GUID for DeleteClient permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("1E4A95A1-C2DB-46B4-A2CC-445DA1E76A8C"), // GUID for CreateClient permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("B6F8C089-740E-4741-8456-6F8D99E9657A"), // GUID for ViewTicket permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("94EBCAB5-660B-4E8D-80C1-8AD58CB7F2C5"), // GUID for EditTicket permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("A4994D1D-091F-44A9-9DF8-793F23634A9B"), // GUID for DeleteTicket permission
            },
            new RolePermission
            {
                RoleId = Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // GUID for Admin role
                PermissionId = Guid.Parse("1A12F6F4-88FF-4AB6-9E44-013CBF5C1964"), // GUID for CreateTicket permission
            }
        );
    }
}