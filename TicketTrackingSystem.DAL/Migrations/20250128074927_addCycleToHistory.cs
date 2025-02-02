using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addCycleToHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CycleNumber",
                table: "TicketHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "721e6628-012c-4069-8b42-4a74dc50bdaf", "AQAAAAIAAYagAAAAEP5Xl9zKcOpEtf9IWofcklzWfqBT7bOFKXM4Yv/hWen3ND1MCJA4cBbmX/esn0WPjw==", "113c0d59-840f-4b05-9297-cb03a899f269" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CycleNumber",
                table: "TicketHistory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6de39531-f3a4-44dd-9cc9-c7555b6f4670", "AQAAAAIAAYagAAAAEC31+hF8APR5kw5xmxgSfXSLnyXZSdWu4IKThkmZicrv2S/lfy2KciskgIGhmXY7sg==", "2d73df3b-ac77-422b-8f3a-4797977646ee" });
        }
    }
}
