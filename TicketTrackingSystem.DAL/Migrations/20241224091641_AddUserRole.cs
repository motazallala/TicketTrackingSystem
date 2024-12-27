using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"), 0, "cfd1c77e-59f6-47b9-8807-8e35e668ea88", null, "sami@example.com", true, "Sami", "Subarna", false, null, "SAMI@EXAMPLE.COM", "SAMISUBARNA", "AQAAAAIAAYagAAAAEJD4UZLxPnJ1w2T1L1jyW5IaiUkeW+hceI9QuMPFvjvx0dmHTPNEilUD9usVqbkZlQ==", null, false, "78050fbe-783a-4d4b-bc82-51f95b34bf26", false, "samisubarna", 0 },
                    { new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"), 0, "e82d158b-9112-4033-b8e8-6bdd9ec514a1", null, "motaz@example.com", true, "Motaz", "Allala", false, null, "MOTAZ@EXAMPLE.COM", "MOTAZALLALA", "AQAAAAIAAYagAAAAEFERs3K/OaKjrQAzeUPiElMx4OovWJOtceAZV7Ujrr3rpp6SuEu5LozQMTmdv+cj0Q==", null, false, "28003eaf-958c-4667-8651-5137103c4d2d", false, "motazallala", 0 }
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
    }
}
