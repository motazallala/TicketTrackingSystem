using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeHistoryImplem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_AspNetUsers_AssignedToId",
                table: "TicketHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_AspNetUsers_UserId",
                table: "TicketHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_TicketHistory_ParentId",
                table: "TicketHistory");

            migrationBuilder.DropIndex(
                name: "IX_TicketHistory_ParentId",
                table: "TicketHistory");

            migrationBuilder.DropIndex(
                name: "IX_TicketHistory_UserId",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "HistoryType",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "StageBeforeChange",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TicketHistory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6de39531-f3a4-44dd-9cc9-c7555b6f4670", "AQAAAAIAAYagAAAAEC31+hF8APR5kw5xmxgSfXSLnyXZSdWu4IKThkmZicrv2S/lfy2KciskgIGhmXY7sg==", "2d73df3b-ac77-422b-8f3a-4797977646ee" });

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_AspNetUsers_AssignedToId",
                table: "TicketHistory",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_AspNetUsers_AssignedToId",
                table: "TicketHistory");

            migrationBuilder.AddColumn<int>(
                name: "ActionName",
                table: "TicketHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HistoryType",
                table: "TicketHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "TicketHistory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StageBeforeChange",
                table: "TicketHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TicketHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_UserId",
                table: "TicketHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_AspNetUsers_AssignedToId",
                table: "TicketHistory",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_AspNetUsers_UserId",
                table: "TicketHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_TicketHistory_ParentId",
                table: "TicketHistory",
                column: "ParentId",
                principalTable: "TicketHistory",
                principalColumn: "Id");
        }
    }
}
