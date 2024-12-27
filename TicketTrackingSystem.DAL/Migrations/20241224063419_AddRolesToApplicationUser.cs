using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfd1c77e-59f6-47b9-8807-8e35e668ea88", "AQAAAAIAAYagAAAAEJD4UZLxPnJ1w2T1L1jyW5IaiUkeW+hceI9QuMPFvjvx0dmHTPNEilUD9usVqbkZlQ==", "78050fbe-783a-4d4b-bc82-51f95b34bf26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e82d158b-9112-4033-b8e8-6bdd9ec514a1", "AQAAAAIAAYagAAAAEFERs3K/OaKjrQAzeUPiElMx4OovWJOtceAZV7Ujrr3rpp6SuEu5LozQMTmdv+cj0Q==", "28003eaf-958c-4667-8651-5137103c4d2d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a4c64d2-f842-4ac1-9809-4f3ae828b66e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3827f08-a5ae-4b65-bdf6-6a1816f54c4f", "AQAAAAIAAYagAAAAEFGDhCxFHBX2vw1+LdFwJoMANldVxXERQkF+MulYvMncR+JZ6ygo/Ltyc+WQcgJkTQ==", "964dfa46-e08d-4c15-b621-635d7033847e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f842bb8-1140-44a0-bfb9-ac8347e30ad0", "AQAAAAIAAYagAAAAEPLlGRZTNhhetyPUV78eTYYCxdhOr4yIGSvOhVv9oVBag2FADSk1xnulWPIT/oyARQ==", "477a9ef8-3820-4cfe-875a-82ad3be1815e" });
        }
    }
}
