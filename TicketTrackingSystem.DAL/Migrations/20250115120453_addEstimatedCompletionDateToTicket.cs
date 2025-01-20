using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addEstimatedCompletionDateToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedCompletionDate",
                table: "Ticket");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "019c87f5-a2e1-4d40-9e78-07d432654d48", "AQAAAAIAAYagAAAAECjfupPJzuoIN+9mc4dA01oGH3s+vR2u7jx40yxDLJOwgraLjPk6+nyETMGDX1IoYA==", "818ecdb4-2c9b-4e3f-8d11-0078a751fe41" });
        }
    }
}
