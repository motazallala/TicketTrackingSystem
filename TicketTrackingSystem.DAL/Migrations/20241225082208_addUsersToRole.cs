using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addUsersToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"), new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0") },
                    { new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"), new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be") }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"), new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"), new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d58ef025-8888-4379-82ac-affabd7a0e1e", "AQAAAAIAAYagAAAAEGH7RUvoFf1b4jIWKhCE4itDXmao0t1LAnjXWQWlMtUkNIzsJieC8JNIgnUpgb+4WQ==", "65bb4f40-3eee-4bc3-a754-d534a064749a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9443ccb5-b414-4bb2-ab1b-4f48d2c316cb", "AQAAAAIAAYagAAAAEGyrCJ7p/pgH4tJeRfIxd0pTHjsjU13C/D5bGWfGqA1tbb1k1v9QK278tC0rLWdc6w==", "01034664-0b1d-4c60-87a6-5ebcf8a8f0fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0455315f-f9c3-4300-af02-e7410d3cae77", "AQAAAAIAAYagAAAAEHv+hSfaLwsFfIErUnJNapyPuNtUj3Ua4116RkFGohfLk9whkenAMuEB5mQPofzJ2A==", "485cf8b9-01e8-4b84-856f-0dae4de5edc6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ddd0ff2-9d28-4d47-bc81-b5a22ba109bc", "AQAAAAIAAYagAAAAEOQLP6hAG1ospvj4spysZn8svwerWpgBLyBtf3NLo02Y5DmjMSTIgOeZx7UBAj1C9Q==", "52c4d29b-2536-4b36-a4d9-61c3dfcd61f9" });
        }
    }
}
