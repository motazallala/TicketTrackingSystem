using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisibleToClient",
                table: "TicketHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "TicketHistory");

            migrationBuilder.RenameColumn(
                name: "StageAtTimeOfMessage",
                table: "TicketHistory",
                newName: "StageBeforeChange");

            migrationBuilder.AddColumn<int>(
                name: "StageAfterChange",
                table: "TicketHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TicketMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StageAtTimeOfMessage = table.Column<int>(type: "int", nullable: false),
                    IsVisibleToClient = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketMessage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketMessage_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50a506e1-48c0-4279-b391-6985c973e186", "AQAAAAIAAYagAAAAEHqFusUdq9rPlUournehQKlx4T/RzX0Fl7uJamUCXuBL+0n2EyOkBU8dIqlV7CgrAw==", "fc98f207-d176-4772-b619-c846f66ed8d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db23623d-4fcb-4dd9-8e67-2caf5e0650f3", "AQAAAAIAAYagAAAAEG9j6YiergvEW7GLUBWd1D1cpLwPJlB+D1e2hWQmlXkz5GlWGBgAvDnMvFa8fZlIZw==", "62cc36a6-f9b3-49ee-8df7-a74ad99cf5ae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "657f17e3-a690-41c4-84d8-7e92ced7dd04", "AQAAAAIAAYagAAAAEFi5NGnelWhmYIuGSzqFvqy7ONaYBjl53pq0jFzmaLzqVJVuUpOD57H6Uy00eZw5WA==", "78769397-ae94-4487-8910-2b33150f5b0a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb115655-3fd9-4055-87b3-714202ca3dc5", "AQAAAAIAAYagAAAAEHxJaes12nRyWUKEY62rqT3K3s4h7IX/vdTpeuJyg3UXVi6Tc+p0q1ZPc08ktvp5ng==", "a6e77859-a731-4ed1-9b07-3a97243620c2" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessage_TicketId",
                table: "TicketMessage",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessage_UserId",
                table: "TicketMessage",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketMessage");

            migrationBuilder.DropColumn(
                name: "StageAfterChange",
                table: "TicketHistory");

            migrationBuilder.RenameColumn(
                name: "StageBeforeChange",
                table: "TicketHistory",
                newName: "StageAtTimeOfMessage");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleToClient",
                table: "TicketHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "TicketHistory",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f903371f-0e8c-4c7c-976a-b5be6f8dc1f4", "AQAAAAIAAYagAAAAEFyNGWk7VVx49OV+b0mRdIjCULYCPyNnOEO/V7bhFMb1KFjI/cpi2julWOSPF5UffQ==", "9da80b12-5241-4f54-93de-b501759f4c0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7642f153-1586-456b-9c74-cc4aa6981933", "AQAAAAIAAYagAAAAEMlZio2fFQLiALjLrTprzmCi+Wm6XkBssXENR4zNrvg6llQvSE5vqtWUT2SLZxLeTw==", "fc33f6e9-5c6b-484d-b42d-5abeaea015bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e4df19c-d334-4723-8131-d694829c899b", "AQAAAAIAAYagAAAAEO09/sOCXg5RKqcVmKpvKlqzbqLUUcZfBJv6gk67KBLMFzw7iVHN7f7aetug5r8PCA==", "3a37353c-4b83-4592-be42-95fe26cfa507" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af140bb3-76ad-4ace-961b-f9903dc81f79", "AQAAAAIAAYagAAAAEPjeF8zd70p3b4KfhPjLZJcZrs95SFMHiWaA0ZFY9aDcWMMSgoFeAp/LfIgJwkHBSw==", "c2b7c6ff-daaf-49c8-b795-48d25c4449d6" });
        }
    }
}
