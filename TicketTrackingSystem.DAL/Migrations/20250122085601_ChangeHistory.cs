using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_TicketHistory_ParentId",
                table: "TicketHistory");

            migrationBuilder.DropIndex(
                name: "IX_TicketHistory_ParentId",
                table: "TicketHistory");

            migrationBuilder.AddColumn<int>(
                name: "ActionName",
                table: "TicketHistory",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b102f3e0-c857-44c5-9a0f-2d0ac2736f31", "AQAAAAIAAYagAAAAEJLzlv7w4cOLywpScCXyvF8J+hg0A11R3hNii7aMe/xkmuFPxhWgefGiPCR2p546gA==", "4615aa72-382f-4928-9b4b-98b2f20854ea" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "TicketHistory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90472466-57a6-4b4d-97de-0cb24f7e1ed5", "AQAAAAIAAYagAAAAEAsmmD+qu+Sp15zmYmFTA0Hg1AbM+R+LjrpBq2IAPlstwD/maoKRFmKn3lSPDARu2A==", "81b9045f-c592-41c0-9c0f-5e6508717183" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_ParentId",
                table: "TicketHistory",
                column: "ParentId",
                unique: true,
                filter: "[ParentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_TicketHistory_ParentId",
                table: "TicketHistory",
                column: "ParentId",
                principalTable: "TicketHistory",
                principalColumn: "Id");
        }
    }
}
