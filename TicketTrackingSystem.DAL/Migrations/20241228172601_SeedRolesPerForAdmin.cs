using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesPerForAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "facf85ad-4792-4d4c-a98b-abdb95ffae8d", "AQAAAAIAAYagAAAAEBXkFzbLPU1/fb6f70aVHCw3hnfudVlXti3cQ69mwqDV0dF3+W15F+ACyzNUOe7mfA==", "49fdfdba-ad65-459a-be1b-fa3f80781506" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0ab34f5-86d6-442f-ba3b-b0dc88eae096", "AQAAAAIAAYagAAAAENIljfhVcsxZC8cfF9pSJ+e5T8PcEXvR5pNsP6f7ATQPiVeezFJzP8EWYSH5z/8g4Q==", "6bbe4fe7-ef83-48ae-ab2f-4c507f0ab94d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db46d8a6-a359-4ccc-9dfc-7b96c45eca09", "AQAAAAIAAYagAAAAEMnl2PCypc8NiFBU23gAAU9oH+uOtB71MOBOXWhYcZQ2KLBjda9D1Qw1pm5EK1Mkkw==", "ff456e34-42b1-41d6-9d66-10710fd57852" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b31b8f1d-135c-48cb-bf8d-f0b9287371fd", "AQAAAAIAAYagAAAAEJk+/V/PmdwR+J2A4N5wgAndvUwzR3grrAoZqghkeJa1Rwoy1ZIJ6EVNFlD+tRWATQ==", "f62ee31c-c9d1-4958-aa91-c85690a6a84e" });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("1a12f6f4-88ff-4ab6-9e44-013cbf5c1964"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("1e4a95a1-c2db-46b4-a2cc-445da1e76a8c"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("4b346e07-abbb-4f62-a3f9-4c109bc153f2"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("5b28fb44-8e97-4a71-b5a2-b041cf58b7d2"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("5e4fdcac-48ff-42fe-b06e-912dbdf73d60"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("6d370d45-c2f0-4691-a80e-91e1de7f9c1c"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("77b7c29f-6b7b-4fc5-937b-37e8aa0e37f4"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("88b6dda7-cfce-4cc5-833d-2f8cf8fea5c4"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("94ebcab5-660b-4e8d-80c1-8ad58cb7f2c5"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("a2282ae2-16b1-43b5-93bd-5f75045e1919"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("a4994d1d-091f-44a9-9df8-793f23634a9b"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("ac132e0d-4d87-4217-a648-b8e97c5f4d6f"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("d634bd07-26c3-421d-8fb7-34747a258af7"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("e79d0c94-7875-4c1e-a93c-77f1fcdb303a"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("f8dde594-4b1b-4a0a-9495-9818a0636de2"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("1a12f6f4-88ff-4ab6-9e44-013cbf5c1964"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("1e4a95a1-c2db-46b4-a2cc-445da1e76a8c"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

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
                keyValues: new object[] { new Guid("4b346e07-abbb-4f62-a3f9-4c109bc153f2"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("5b28fb44-8e97-4a71-b5a2-b041cf58b7d2"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("5e4fdcac-48ff-42fe-b06e-912dbdf73d60"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6d370d45-c2f0-4691-a80e-91e1de7f9c1c"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("77b7c29f-6b7b-4fc5-937b-37e8aa0e37f4"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("88b6dda7-cfce-4cc5-833d-2f8cf8fea5c4"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("94ebcab5-660b-4e8d-80c1-8ad58cb7f2c5"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a2282ae2-16b1-43b5-93bd-5f75045e1919"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a4994d1d-091f-44a9-9df8-793f23634a9b"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ac132e0d-4d87-4217-a648-b8e97c5f4d6f"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d634bd07-26c3-421d-8fb7-34747a258af7"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e79d0c94-7875-4c1e-a93c-77f1fcdb303a"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f8dde594-4b1b-4a0a-9495-9818a0636de2"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d5bff20-0afa-4d11-b095-2771ac69ca88", "AQAAAAIAAYagAAAAEIpKYrh27r0scTWtT5U60ocnZYTvWbd64yk18PGeYNztgQhj425VDtvAB0+InlXrsA==", "9a92070c-3249-4cbc-a5a8-af9d0a578715" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e953590-5ee9-4a9f-8e55-9d8c6506f385", "AQAAAAIAAYagAAAAEO4hx/4Rxz8z0x611f0l25wEqNMheV0ECsWD3aBg6LdtI3zURmdFX7UtFAFHx3jw3Q==", "27be510d-a989-4ae0-9605-69b0694ad188" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4f78d6b-dec8-40eb-a950-350ca567dc57", "AQAAAAIAAYagAAAAEM4nAbq5eBcByM9tu3OABHh5pU9yHVnSeuJ1po8XG8ZiCod0BgSzdfWSpGrK+aMvyg==", "4d634d4f-4dac-4223-8910-e030b1951646" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38abd211-7815-4d99-aa69-a2d8acfd23c8", "AQAAAAIAAYagAAAAEGlLoUyG/aWoJq2WimmiqHIoFjeY//nCVVBIHx8g/EJmpC/GvdSXvLSQ/CZQIXmpWA==", "37f0f03a-c124-42a1-8292-e8cc7b9346aa" });
        }
    }
}
