using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeRoles : Migration
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
                keyValues: new object[] { new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"), new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"), new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"), new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afac6002-11f1-481d-981d-9dc635da3380", "AQAAAAIAAYagAAAAEHVywKMgCNV8f8S2/4Y2z2IvNhzd261AQzBshNoB7HZePOamkQo3GhqX1LnI+Xb1JQ==", "db127288-dde4-4683-85b8-19be8bab9047" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c4ecce3-bd5c-40b4-bbf9-bd25fad0de98", "AQAAAAIAAYagAAAAEDyGSAjpeE0iVC7OWi2iZJSbTTuWxBWYR7EKe8BoLas3cMMFuf0A8sbvsZ7PuGG7aQ==", "5642fbd8-9718-4448-9df5-7c717f1573cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9dcfdbf9-3738-4b87-9fbb-ddc146f48c1e", "AQAAAAIAAYagAAAAEIPFOsF0Or0ZCZHfQA71ItVp5TTgd8glf5e4m/lXgR1hC5oc078XyFmd5P8Ne17Hag==", "d13b2efc-6065-471d-a0f4-3a0d69ff591f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0ae3d16-211c-4e4c-ad39-8ef7abd414c6", "AQAAAAIAAYagAAAAEEC4rbB0kJErTL3qYgb0iwMKNWlAEf7zQ+1erCC4sPt9DgIjd7tpn8InB0xFP5aung==", "92644de3-404d-4242-92c1-7355e2c51625" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "isDeleted" },
                values: new object[,]
                {
                    { new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), null, "User", "USER", false },
                    { new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"), null, "DevelopmentDepartment", "DEVElOPMENTDEPARTMENT", false },
                    { new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"), null, "BusinessAnalyses", "BUSINESSANAlYSES", false }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50a506e1-48c0-4279-b391-6985c973e186", "AQAAAAIAAYagAAAAEHqFusUdq9rPlUournehQKlx4T/RzX0Fl7uJamUCXuBL+0n2EyOkBU8dIqlV7CgrAw==", "fc98f207-d176-4772-b619-c846f66ed8d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db23623d-4fcb-4dd9-8e67-2caf5e0650f3", "AQAAAAIAAYagAAAAEG9j6YiergvEW7GLUBWd1D1cpLwPJlB+D1e2hWQmlXkz5GlWGBgAvDnMvFa8fZlIZw==", "62cc36a6-f9b3-49ee-8df7-a74ad99cf5ae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "657f17e3-a690-41c4-84d8-7e92ced7dd04", "AQAAAAIAAYagAAAAEFi5NGnelWhmYIuGSzqFvqy7ONaYBjl53pq0jFzmaLzqVJVuUpOD57H6Uy00eZw5WA==", "78769397-ae94-4487-8910-2b33150f5b0a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb115655-3fd9-4055-87b3-714202ca3dc5", "AQAAAAIAAYagAAAAEHxJaes12nRyWUKEY62rqT3K3s4h7IX/vdTpeuJyg3UXVi6Tc+p0q1ZPc08ktvp5ng==", "a6e77859-a731-4ed1-9b07-3a97243620c2" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"), new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e") },
                    { new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"), new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0") },
                    { new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"), new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4") },
                    { new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"), new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f") }
                });
        }
    }
}
