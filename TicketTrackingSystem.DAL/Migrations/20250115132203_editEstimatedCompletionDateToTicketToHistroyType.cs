using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editEstimatedCompletionDateToTicketToHistroyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedCompletionDate",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "HistoryType",
                table: "Ticket",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2388e9e1-e0c8-4990-b420-40184dadf919", "AQAAAAIAAYagAAAAEOl4tvHLdsa2Vc74w4YOneJ1/hCkpBDwMalRtCZ7AKkSAKvP3OpHyxbvLqB0nIdEjQ==", "027c8676-c997-46b0-a403-2084c6f6d653" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoryType",
                table: "Ticket");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedCompletionDate",
                table: "Ticket",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ddbe389d-6cc8-421b-8ae8-119c2587ed40", "AQAAAAIAAYagAAAAENnwlHRXWZRXuD2MuTM++5YG4BzDZ0r/KlNgEvMcJnOtPIiYvpxCM7wp1Y/Cr8QTeg==", "f60edef3-9b0c-469c-bb3f-a5767096d935" });
        }
    }
}
