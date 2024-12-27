using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addSoftDeleteForRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"),
                column: "isDeleted",
                value: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e70a61a-f2fc-4e7c-9aad-8171f0193493", "AQAAAAIAAYagAAAAEAluhe52JefWzIpIehCV5PwrQaFWTJNenwzW0i9KAwiwrNJwmlaEcrclY+Ucq3R4bw==", "5a335be9-b270-4820-bd79-5606d465184a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ecb5ca91-90d3-45a4-b2d1-ad0b81099d6e", "AQAAAAIAAYagAAAAEKPA7ulSMH9H8yYFA6+YZFUVjB1ICFWLf/T7w+Oki9Gaa6KbAz4C2GCMrJOzkeFjMQ==", "568ed90d-4832-401f-aa28-811c339c6f6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7b92011-1ac9-4063-bd91-683d84f49269", "AQAAAAIAAYagAAAAEKpDWWwWA3pfidwbLDfCi+3V9fOuC3ToBQOj67PeVRAjk3k75MhKBBRxgvUXlRSuOg==", "1bb60330-090f-4782-8b63-e8b417123642" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2423e6f-bf14-4b76-9df3-46a9d068ceaf", "AQAAAAIAAYagAAAAEBrrXYE9BDwLREHh41egZMXsAzi4a7F43Zsgo1GIs6er6GcsmaEFRR9Dnw1jZZX6Dg==", "b6f31767-b01e-4ef1-b5bf-1d6f43fb66f5" });
        }
    }
}
