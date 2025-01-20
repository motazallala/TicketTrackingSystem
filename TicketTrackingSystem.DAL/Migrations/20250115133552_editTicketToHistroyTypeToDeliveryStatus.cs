using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editTicketToHistroyTypeToDeliveryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HistoryType",
                table: "Ticket",
                newName: "DeliveryStatus");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5312a080-0429-454a-8cba-2cc53508e322", "AQAAAAIAAYagAAAAEL6fJpwruRnHddwGlZjEoVwmYu3P7K8FDgU5kSUKOxlYI7YLEidggJD0DK3ilUWfyQ==", "1f2a5167-0305-4a69-8ee5-04c866225d61" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryStatus",
                table: "Ticket",
                newName: "HistoryType");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2388e9e1-e0c8-4990-b420-40184dadf919", "AQAAAAIAAYagAAAAEOl4tvHLdsa2Vc74w4YOneJ1/hCkpBDwMalRtCZ7AKkSAKvP3OpHyxbvLqB0nIdEjQ==", "027c8676-c997-46b0-a403-2084c6f6d653" });
        }
    }
}
