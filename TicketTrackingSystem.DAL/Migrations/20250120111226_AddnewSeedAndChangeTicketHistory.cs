using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddnewSeedAndChangeTicketHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "TicketHistory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90472466-57a6-4b4d-97de-0cb24f7e1ed5", "AQAAAAIAAYagAAAAEAsmmD+qu+Sp15zmYmFTA0Hg1AbM+R+LjrpBq2IAPlstwD/maoKRFmKn3lSPDARu2A==", "81b9045f-c592-41c0-9c0f-5e6508717183" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("200a2920-d8fd-4d10-9ff9-30a6156a5879"), false, "ViewTicketReport", null },
                    { new Guid("3e353a51-e46d-446c-a385-38d8f6ddeb79"), false, "ViewTicketMessage", null },
                    { new Guid("4c93ec77-b8a5-4b58-aa26-1d854e81b666"), false, "DeleteTicketMessage", new Guid("3e353a51-e46d-446c-a385-38d8f6ddeb79") },
                    { new Guid("607aa269-da9e-498c-9ce6-9cf380583542"), false, "EditTicketMessage", new Guid("3e353a51-e46d-446c-a385-38d8f6ddeb79") },
                    { new Guid("6170d82b-9e6f-41db-9bcf-c4c265bf0f37"), false, "DeleteTicketReport", new Guid("200a2920-d8fd-4d10-9ff9-30a6156a5879") },
                    { new Guid("6d774c98-7bd9-40a4-a735-2c433c1f2d03"), false, "CreateTicketReport", new Guid("200a2920-d8fd-4d10-9ff9-30a6156a5879") },
                    { new Guid("8dc4ddc3-c1e4-4e32-a1e0-508baa89b3db"), false, "EditTicketReport", new Guid("200a2920-d8fd-4d10-9ff9-30a6156a5879") },
                    { new Guid("ad7c90eb-083d-4a09-b99e-6bd25e30c5b3"), false, "CreateTicketMessage", new Guid("3e353a51-e46d-446c-a385-38d8f6ddeb79") }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_TicketHistory_ParentId",
                table: "TicketHistory");

            migrationBuilder.DropIndex(
                name: "IX_TicketHistory_ParentId",
                table: "TicketHistory");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("4c93ec77-b8a5-4b58-aa26-1d854e81b666"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("607aa269-da9e-498c-9ce6-9cf380583542"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("6170d82b-9e6f-41db-9bcf-c4c265bf0f37"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("6d774c98-7bd9-40a4-a735-2c433c1f2d03"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("8dc4ddc3-c1e4-4e32-a1e0-508baa89b3db"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("ad7c90eb-083d-4a09-b99e-6bd25e30c5b3"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("200a2920-d8fd-4d10-9ff9-30a6156a5879"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("3e353a51-e46d-446c-a385-38d8f6ddeb79"));

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "TicketHistory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5312a080-0429-454a-8cba-2cc53508e322", "AQAAAAIAAYagAAAAEL6fJpwruRnHddwGlZjEoVwmYu3P7K8FDgU5kSUKOxlYI7YLEidggJD0DK3ilUWfyQ==", "1f2a5167-0305-4a69-8ee5-04c866225d61" });
        }
    }
}
