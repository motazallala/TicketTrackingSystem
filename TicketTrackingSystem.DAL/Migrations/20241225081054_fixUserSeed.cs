using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9443ccb5-b414-4bb2-ab1b-4f48d2c316cb", "SAMIDD@EXAMPLE.COM", "SAMISUBARNADD", "AQAAAAIAAYagAAAAEGyrCJ7p/pgH4tJeRfIxd0pTHjsjU13C/D5bGWfGqA1tbb1k1v9QK278tC0rLWdc6w==", "01034664-0b1d-4c60-87a6-5ebcf8a8f0fd" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52c7b126-4c1b-41d0-af6a-facc6606ceb9", "AQAAAAIAAYagAAAAEF8vOB0AkbuFwVyHBrUqJAVm3NigQYJBTt+fwPlE7njI4s1EFO6MBf3pfDOQNojnUg==", "34c79d13-b001-4d61-8761-7905b5aaced9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0368f55-3aa4-4913-b14e-efc94b6185cf", "SAMIBA@EXAMPLE.COM", "SAMISUBARNABADD", "AQAAAAIAAYagAAAAEOlBV9XynWmVUDHUhtptVyLRZ1tkAsWNOdwKgsGJBtf3piuPtDjpC7eXizCfLtAX1Q==", "ee7b4b7a-31ef-4cbb-8a53-ef2e063849ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a17be0c-97be-4beb-b84e-1f2d8c42198e", "AQAAAAIAAYagAAAAEFtW3Tj72wY26oJIN3mqJ5YLIDWtHWV5Xp70sKUy5f4tY6dHv+J1Weww33Y+nekPnA==", "ab0bed53-5b10-4674-ad6a-e795463484f3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87d8c847-fd32-4bcd-9bae-1517ee287b20", "AQAAAAIAAYagAAAAELP6R+kIh1pgppr9sB1snnSlaCj2+9W8/ZZCkhaBJcL8PYskCrHq/2ZxeRL/D3BO8w==", "5ca3f4c4-dee3-40dd-8e07-c4416c4d08bd" });
        }
    }
}
