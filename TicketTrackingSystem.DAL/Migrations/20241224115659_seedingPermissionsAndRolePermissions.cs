using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedingPermissionsAndRolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b493d2c-715f-4b30-91b3-4851b6f12159", "AQAAAAIAAYagAAAAEFzyxGnRBUrToNYlFV4GXG1IN6o/9BwvfjCSVQ5uDcv6I2ll/vxSdpPqOTM61caAqg==", "621872d2-4a15-4bec-97b8-6f6184fab69e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2489c44-474d-487b-adee-0d9220d4e0ca", "AQAAAAIAAYagAAAAEHLJFd3zK+iBe8z8agGfVnzQh03vqWV2/UTJ2OGzKuqQgJWlaAJ6avyolRfZkrOhWQ==", "532c8916-f456-49a5-9c2f-2da2b58ec93e" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"), false, "Delete" },
                    { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), false, "View" },
                    { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), false, "Edit" },
                    { new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"), false, "Create" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "PageName" },
                values: new object[,]
                {
                    { new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), 0 },
                    { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), 0 },
                    { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), 0 },
                    { new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), 0 },
                    { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), 0 },
                    { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4") });

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("2f618f69-b884-4006-a916-8131f341f0b3"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c306e94e-c2fb-4da8-accb-f0b09968916c", "AQAAAAIAAYagAAAAECrKyTYCwrnY3p0SrMYusnpFY7Xagu4Z/VdOXq6XKEaAVxnhG6sU0JLwJkbJGQ3Kng==", "78336ec5-1e88-47dd-8ec3-bea19f808fb4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97e0e0f1-7448-4621-92d0-fbc5b288696b", "AQAAAAIAAYagAAAAEJM2PNMgapQXdWvTNXUEXQfHPfAwU0htv1xcXIQknGX7uGTIvvJ7pQRgEG6S0boVxw==", "c69a9bd2-d5d9-4ce2-b1c3-311e760af084" });
        }
    }
}
