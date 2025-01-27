using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHistoryToManyRe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9787d334-fe74-44af-b3ef-349f11658256", "AQAAAAIAAYagAAAAEGO5FNQK/MR7x1lphXI9ziLKopsYMiARQx0Eds/YEWCTvGNyFd4Hu4DVSoVb4fWmag==", "046f6f97-ad43-4f73-9f30-74d5d96cfee3" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_ParentId",
                table: "TicketHistory",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_TicketHistory_ParentId",
                table: "TicketHistory",
                column: "ParentId",
                principalTable: "TicketHistory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_TicketHistory_ParentId",
                table: "TicketHistory");

            migrationBuilder.DropIndex(
                name: "IX_TicketHistory_ParentId",
                table: "TicketHistory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b102f3e0-c857-44c5-9a0f-2d0ac2736f31", "AQAAAAIAAYagAAAAEJLzlv7w4cOLywpScCXyvF8J+hg0A11R3hNii7aMe/xkmuFPxhWgefGiPCR2p546gA==", "4615aa72-382f-4928-9b4b-98b2f20854ea" });
        }
    }
}
