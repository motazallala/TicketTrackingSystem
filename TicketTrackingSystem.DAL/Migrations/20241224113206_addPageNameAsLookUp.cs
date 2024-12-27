using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addPageNameAsLookUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PageName",
                table: "RolePermission",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PageName",
                table: "RolePermission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a13cb8a0-7d12-46b8-9951-fc7759d41a9e", "AQAAAAIAAYagAAAAEFsFXjelD2+ctC9/EzIyPmyS68Hx+VWm8CeUf0xxF/5Cn4CSF8Sm3WfDGkz7qcOIsg==", "99361e30-758d-43fe-8a97-0af1755286f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a2420ce-315a-49cc-aec9-47f3c0d6f8fb", "AQAAAAIAAYagAAAAEPX/vE2GR5jvqjCQmbl4UW2vxsctPuzED3fS0omsJrdWzU7Q9Ifw1Vfydp44y3RR1Q==", "8596936e-7990-4aab-b90a-3674f5a6e78c" });
        }
    }
}
