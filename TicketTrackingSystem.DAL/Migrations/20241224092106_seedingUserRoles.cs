using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedingUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), null, "Admin", "ADMIN" },
                    { new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"), 0, "a13cb8a0-7d12-46b8-9951-fc7759d41a9e", null, "sami@example.com", true, "Sami", "Subarna", false, null, "SAMI@EXAMPLE.COM", "SAMISUBARNA", "AQAAAAIAAYagAAAAEFsFXjelD2+ctC9/EzIyPmyS68Hx+VWm8CeUf0xxF/5Cn4CSF8Sm3WfDGkz7qcOIsg==", null, false, "99361e30-758d-43fe-8a97-0af1755286f9", false, "samisubarna", 0 },
                    { new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"), 0, "7a2420ce-315a-49cc-aec9-47f3c0d6f8fb", null, "motaz@example.com", true, "Motaz", "Allala", false, null, "MOTAZ@EXAMPLE.COM", "MOTAZALLALA", "AQAAAAIAAYagAAAAEPX/vE2GR5jvqjCQmbl4UW2vxsctPuzED3fS0omsJrdWzU7Q9Ifw1Vfydp44y3RR1Q==", null, false, "8596936e-7990-4aab-b90a-3674f5a6e78c", false, "motazallala", 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e") },
                    { new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"));
        }
    }
}
