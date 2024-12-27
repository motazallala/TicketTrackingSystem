using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeLogicOfPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageName",
                table: "RolePermission");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Permission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5b68c12-cb83-4115-a430-19ed4969cc46", "AQAAAAIAAYagAAAAEDWkt0OqfUgGHB3SDYsg6+ZvOgQN7mgMw6KgKIKpOZrwBE7J6Ml5qjRspyfqLVwj/w==", "b0415ed6-d4ff-4ecb-b777-f38b7059c61a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c0e54bc-7315-4933-bb8f-e2750db70fa4", "AQAAAAIAAYagAAAAEBJ+On+VWb4ZyMDWOI/urQYSNGIai0qmWv32/gbk4PkC1RQNJjVXC42Ko1wOfLo/Nw==", "6dd044a5-3e2a-43f9-9de2-c9b487f73d24" });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"),
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("2f618f69-b884-4006-a916-8131f341f0b3"),
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"),
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"),
                column: "ParentId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ParentId",
                table: "Permission",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Permission_ParentId",
                table: "Permission",
                column: "ParentId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_Permission_ParentId",
                table: "Permission");

            migrationBuilder.DropIndex(
                name: "IX_Permission_ParentId",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Permission");

            migrationBuilder.AddColumn<int>(
                name: "PageName",
                table: "RolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b493d2c-715f-4b30-91b3-4851b6f12159", "AQAAAAIAAYagAAAAEFzyxGnRBUrToNYlFV4GXG1IN6o/9BwvfjCSVQ5uDcv6I2ll/vxSdpPqOTM61caAqg==", "621872d2-4a15-4bec-97b8-6f6184fab69e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2489c44-474d-487b-adee-0d9220d4e0ca", "AQAAAAIAAYagAAAAEHLJFd3zK+iBe8z8agGfVnzQh03vqWV2/UTJ2OGzKuqQgJWlaAJ6avyolRfZkrOhWQ==", "532c8916-f456-49a5-9c2f-2da2b58ec93e" });

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                column: "PageName",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                column: "PageName",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                column: "PageName",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                column: "PageName",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4") },
                column: "PageName",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4") },
                column: "PageName",
                value: 0);
        }
    }
}
