using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedingPermissionsAndRolePermissionsWithNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4") });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"), null, "DevelopmentDepartment", "DEVElOPMENTDEPARTMENT" },
                    { new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"), null, "BusinessAnalyses", "BUSINESSANAlYSES" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52c7b126-4c1b-41d0-af6a-facc6606ceb9", "AQAAAAIAAYagAAAAEF8vOB0AkbuFwVyHBrUqJAVm3NigQYJBTt+fwPlE7njI4s1EFO6MBf3pfDOQNojnUg==", "34c79d13-b001-4d61-8761-7905b5aaced9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87d8c847-fd32-4bcd-9bae-1517ee287b20", "AQAAAAIAAYagAAAAELP6R+kIh1pgppr9sB1snnSlaCj2+9W8/ZZCkhaBJcL8PYskCrHq/2ZxeRL/D3BO8w==", "5ca3f4c4-dee3-40dd-8e07-c4416c4d08bd" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"), 0, "f0368f55-3aa4-4913-b14e-efc94b6185cf", null, "samiDD@example.com", true, "Sami", "Subarna", false, null, "SAMIBA@EXAMPLE.COM", "SAMISUBARNABADD", "AQAAAAIAAYagAAAAEOlBV9XynWmVUDHUhtptVyLRZ1tkAsWNOdwKgsGJBtf3piuPtDjpC7eXizCfLtAX1Q==", null, false, "ee7b4b7a-31ef-4cbb-8a53-ef2e063849ee", false, "samisubarnaDD", 0 },
                    { new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"), 0, "6a17be0c-97be-4beb-b84e-1f2d8c42198e", null, "samiBA@example.com", true, "Sami", "Subarna", false, null, "SAMIBA@EXAMPLE.COM", "SAMISUBARNABA", "AQAAAAIAAYagAAAAEFtW3Tj72wY26oJIN3mqJ5YLIDWtHWV5Xp70sKUy5f4tY6dHv+J1Weww33Y+nekPnA==", null, false, "ab0bed53-5b10-4674-ad6a-e795463484f3", false, "samisubarnaBA", 0 }
                });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "DeleteProject", new Guid("2f618f69-b884-4006-a916-8131f341f0b3") });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("2f618f69-b884-4006-a916-8131f341f0b3"),
                column: "Name",
                value: "ViewProject");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "EditProject", new Guid("2f618f69-b884-4006-a916-8131f341f0b3") });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "CreateProject", new Guid("2f618f69-b884-4006-a916-8131f341f0b3") });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8"), false, "ViewDepartment", null },
                    { new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a"), false, "ViewTicket", null },
                    { new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9"), false, "ViewClient", null },
                    { new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"), false, "ViewUser", null },
                    { new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7"), false, "ViewRole", null },
                    { new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb"), false, "ViewPermission", null },
                    { new Guid("1a12f6f4-88ff-4ab6-9e44-013cbf5c1964"), false, "CreateTicket", new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a") },
                    { new Guid("1e4a95a1-c2db-46b4-a2cc-445da1e76a8c"), false, "CreateClient", new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9") },
                    { new Guid("30a62382-8c8f-4a13-8806-88104f9f6066"), false, "CreateUser", new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4") },
                    { new Guid("4b346e07-abbb-4f62-a3f9-4c109bc153f2"), false, "EditPermission", new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb") },
                    { new Guid("5b28fb44-8e97-4a71-b5a2-b041cf58b7d2"), false, "CreatePermission", new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb") },
                    { new Guid("5e4fdcac-48ff-42fe-b06e-912dbdf73d60"), false, "EditDepartment", new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8") },
                    { new Guid("6d370d45-c2f0-4691-a80e-91e1de7f9c1c"), false, "CreateRole", new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7") },
                    { new Guid("77b7c29f-6b7b-4fc5-937b-37e8aa0e37f4"), false, "CreateDepartment", new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8") },
                    { new Guid("88b6dda7-cfce-4cc5-833d-2f8cf8fea5c4"), false, "DeleteRole", new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7") },
                    { new Guid("94ebcab5-660b-4e8d-80c1-8ad58cb7f2c5"), false, "EditTicket", new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a") },
                    { new Guid("97f0e5b7-4895-43a5-b831-daf84374a752"), false, "EditUser", new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4") },
                    { new Guid("a2282ae2-16b1-43b5-93bd-5f75045e1919"), false, "EditClient", new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9") },
                    { new Guid("a4994d1d-091f-44a9-9df8-793f23634a9b"), false, "DeleteTicket", new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a") },
                    { new Guid("ac132e0d-4d87-4217-a648-b8e97c5f4d6f"), false, "EditRole", new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7") },
                    { new Guid("d634bd07-26c3-421d-8fb7-34747a258af7"), false, "DeletePermission", new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb") },
                    { new Guid("e19eec9b-0c4b-4e8e-83cb-95e62a51c1b3"), false, "DeleteUser", new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4") },
                    { new Guid("e79d0c94-7875-4c1e-a93c-77f1fcdb303a"), false, "DeleteClient", new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9") },
                    { new Guid("f8dde594-4b1b-4a0a-9495-9818a0636de2"), false, "DeleteDepartment", new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"), new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f") },
                    { new Guid("30a62382-8c8f-4a13-8806-88104f9f6066"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("97f0e5b7-4895-43a5-b831-daf84374a752"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("e19eec9b-0c4b-4e8e-83cb-95e62a51c1b3"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("deb2a077-7a07-49f4-bdda-3c7f95061d72"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("73a03b91-0b28-4838-9d48-c30b7ace75a0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9229f7aa-5b2f-4b72-bdb3-6f786a0c62be"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("1a12f6f4-88ff-4ab6-9e44-013cbf5c1964"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("1e4a95a1-c2db-46b4-a2cc-445da1e76a8c"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("4b346e07-abbb-4f62-a3f9-4c109bc153f2"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("5b28fb44-8e97-4a71-b5a2-b041cf58b7d2"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("5e4fdcac-48ff-42fe-b06e-912dbdf73d60"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("6d370d45-c2f0-4691-a80e-91e1de7f9c1c"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("77b7c29f-6b7b-4fc5-937b-37e8aa0e37f4"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("88b6dda7-cfce-4cc5-833d-2f8cf8fea5c4"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("94ebcab5-660b-4e8d-80c1-8ad58cb7f2c5"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("a2282ae2-16b1-43b5-93bd-5f75045e1919"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("a4994d1d-091f-44a9-9df8-793f23634a9b"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("ac132e0d-4d87-4217-a648-b8e97c5f4d6f"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("d634bd07-26c3-421d-8fb7-34747a258af7"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("e79d0c94-7875-4c1e-a93c-77f1fcdb303a"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("f8dde594-4b1b-4a0a-9495-9818a0636de2"));

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("30a62382-8c8f-4a13-8806-88104f9f6066"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("97f0e5b7-4895-43a5-b831-daf84374a752"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e19eec9b-0c4b-4e8e-83cb-95e62a51c1b3"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"), new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e143ef8a-95c2-4359-b1b6-7fde456b771f"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("30a62382-8c8f-4a13-8806-88104f9f6066"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("773f9a24-48eb-46cf-b6cb-c0b607a85bc8"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("97f0e5b7-4895-43a5-b831-daf84374a752"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("b6f8c089-740e-4741-8456-6f8d99e9657a"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("ca4eab94-88d5-4fd9-a4a2-5eaf8ad927c9"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("e19eec9b-0c4b-4e8e-83cb-95e62a51c1b3"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("ea4085fe-bcc9-44ac-b3f6-3d3f1055b6a7"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("f595dbe8-8e15-4d3f-8a37-85a5e710fbbb"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("d19358c8-5ce7-4c13-8b37-ebfd676e8eb4"));

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
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Delete", null });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("2f618f69-b884-4006-a916-8131f341f0b3"),
                column: "Name",
                value: "View");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Edit", null });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Create", null });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("2702c229-4df3-4d01-99df-db962cb93c3d"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("80418a92-7e17-4b3d-880a-42bf0df503cb"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("f4ced4d6-6844-483d-8b18-a0941a5c266e"), new Guid("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57") },
                    { new Guid("2f618f69-b884-4006-a916-8131f341f0b3"), new Guid("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4") }
                });
        }
    }
}
