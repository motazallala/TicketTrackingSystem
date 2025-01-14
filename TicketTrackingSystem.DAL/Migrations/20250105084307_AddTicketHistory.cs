using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "ProjectMember");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Ticket",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ticket",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permission",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Department",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TicketHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageAtTimeOfMessage = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsVisibleToClient = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketHistory_Ticket_TicketId",
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
                values: new object[] { "f903371f-0e8c-4c7c-976a-b5be6f8dc1f4", "AQAAAAIAAYagAAAAEFyNGWk7VVx49OV+b0mRdIjCULYCPyNnOEO/V7bhFMb1KFjI/cpi2julWOSPF5UffQ==", "9da80b12-5241-4f54-93de-b501759f4c0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "7642f153-1586-456b-9c74-cc4aa6981933", "AQAAAAIAAYagAAAAEMlZio2fFQLiALjLrTprzmCi+Wm6XkBssXENR4zNrvg6llQvSE5vqtWUT2SLZxLeTw==", "fc33f6e9-5c6b-484d-b42d-5abeaea015bd", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "8e4df19c-d334-4723-8131-d694829c899b", "AQAAAAIAAYagAAAAEO09/sOCXg5RKqcVmKpvKlqzbqLUUcZfBJv6gk67KBLMFzw7iVHN7f7aetug5r8PCA==", "3a37353c-4b83-4592-be42-95fe26cfa507", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "af140bb3-76ad-4ace-961b-f9903dc81f79", "AQAAAAIAAYagAAAAEPjeF8zd70p3b4KfhPjLZJcZrs95SFMHiWaA0ZFY9aDcWMMSgoFeAp/LfIgJwkHBSw==", "c2b7c6ff-daaf-49c8-b795-48d25c4449d6", 1 });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("0334a2fc-4dc3-48af-bf24-32f35cc192d8"), false, "ViewTicketHistory", null },
                    { new Guid("15699509-04eb-40e5-a5d4-5effe8f339dd"), false, "EditTicketHistory", new Guid("0334a2fc-4dc3-48af-bf24-32f35cc192d8") },
                    { new Guid("7b70fd1b-58e4-450a-b0cf-b99c040ac9a4"), false, "DeleteTicketHistory", new Guid("0334a2fc-4dc3-48af-bf24-32f35cc192d8") },
                    { new Guid("ec43eeff-88c2-4c22-ad71-71ba7f2cda44"), false, "CreateTicketHistory", new Guid("0334a2fc-4dc3-48af-bf24-32f35cc192d8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_TicketId",
                table: "TicketHistory",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_UserId",
                table: "TicketHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketHistory");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("15699509-04eb-40e5-a5d4-5effe8f339dd"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("7b70fd1b-58e4-450a-b0cf-b99c040ac9a4"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("ec43eeff-88c2-4c22-ad71-71ba7f2cda44"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("0334a2fc-4dc3-48af-bf24-32f35cc192d8"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "ProjectMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Department",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "facf85ad-4792-4d4c-a98b-abdb95ffae8d", "AQAAAAIAAYagAAAAEBXkFzbLPU1/fb6f70aVHCw3hnfudVlXti3cQ69mwqDV0dF3+W15F+ACyzNUOe7mfA==", "49fdfdba-ad65-459a-be1b-fa3f80781506" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "b0ab34f5-86d6-442f-ba3b-b0dc88eae096", "AQAAAAIAAYagAAAAENIljfhVcsxZC8cfF9pSJ+e5T8PcEXvR5pNsP6f7ATQPiVeezFJzP8EWYSH5z/8g4Q==", "6bbe4fe7-ef83-48ae-ab2f-4c507f0ab94d", 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "db46d8a6-a359-4ccc-9dfc-7b96c45eca09", "AQAAAAIAAYagAAAAEMnl2PCypc8NiFBU23gAAU9oH+uOtB71MOBOXWhYcZQ2KLBjda9D1Qw1pm5EK1Mkkw==", "ff456e34-42b1-41d6-9d66-10710fd57852", 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "b31b8f1d-135c-48cb-bf8d-f0b9287371fd", "AQAAAAIAAYagAAAAEJk+/V/PmdwR+J2A4N5wgAndvUwzR3grrAoZqghkeJa1Rwoy1ZIJ6EVNFlD+tRWATQ==", "f62ee31c-c9d1-4958-aa91-c85690a6a84e", 0 });
        }
    }
}
