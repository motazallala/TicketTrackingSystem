using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeSeedAndModifyTicketAndHistoryAndMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"));

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "TicketMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "StageBeforeChange",
                table: "TicketHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StageAfterChange",
                table: "TicketHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToId",
                table: "TicketHistory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                table: "TicketHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedCompletionDate",
                table: "TicketHistory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HistoryType",
                table: "TicketHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "019c87f5-a2e1-4d40-9e78-07d432654d48", "AQAAAAIAAYagAAAAECjfupPJzuoIN+9mc4dA01oGH3s+vR2u7jx40yxDLJOwgraLjPk6+nyETMGDX1IoYA==", "818ecdb4-2c9b-4e3f-8d11-0078a751fe41" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_AssignedToId",
                table: "TicketHistory",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AssignedToId",
                table: "Ticket",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_AspNetUsers_AssignedToId",
                table: "Ticket",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_AspNetUsers_AssignedToId",
                table: "TicketHistory",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_AspNetUsers_AssignedToId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_AspNetUsers_AssignedToId",
                table: "TicketHistory");

            migrationBuilder.DropIndex(
                name: "IX_TicketHistory_AssignedToId",
                table: "TicketHistory");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_AssignedToId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "TicketMessage");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "EstimatedCompletionDate",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "HistoryType",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "StageBeforeChange",
                table: "TicketHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StageAfterChange",
                table: "TicketHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0ae3d16-211c-4e4c-ad39-8ef7abd414c6", "AQAAAAIAAYagAAAAEEC4rbB0kJErTL3qYgb0iwMKNWlAEf7zQ+1erCC4sPt9DgIjd7tpn8InB0xFP5aung==", "92644de3-404d-4242-92c1-7355e2c51625" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"), 0, "afac6002-11f1-481d-981d-9dc635da3380", null, "sami@example.com", true, "Sami", "Subarna", false, null, "SAMI@EXAMPLE.COM", "SAMISUBARNA", "AQAAAAIAAYagAAAAEHVywKMgCNV8f8S2/4Y2z2IvNhzd261AQzBshNoB7HZePOamkQo3GhqX1LnI+Xb1JQ==", null, false, "db127288-dde4-4683-85b8-19be8bab9047", false, "samisubarna", 0 },
                    { new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"), 0, "2c4ecce3-bd5c-40b4-bbf9-bd25fad0de98", null, "samiDD@example.com", true, "Sami", "Subarna", false, null, "SAMIDD@EXAMPLE.COM", "SAMISUBARNADD", "AQAAAAIAAYagAAAAEDyGSAjpeE0iVC7OWi2iZJSbTTuWxBWYR7EKe8BoLas3cMMFuf0A8sbvsZ7PuGG7aQ==", null, false, "5642fbd8-9718-4448-9df5-7c717f1573cc", false, "samisubarnaDD", 1 },
                    { new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"), 0, "9dcfdbf9-3738-4b87-9fbb-ddc146f48c1e", null, "samiBA@example.com", true, "Sami", "Subarna", false, null, "SAMIBA@EXAMPLE.COM", "SAMISUBARNABA", "AQAAAAIAAYagAAAAEIPFOsF0Or0ZCZHfQA71ItVp5TTgd8glf5e4m/lXgR1hC5oc078XyFmd5P8Ne17Hag==", null, false, "d13b2efc-6065-471d-a0f4-3a0d69ff591f", false, "samisubarnaBA", 1 }
                });
        }
    }
}
