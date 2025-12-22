using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDefaultAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessGroupId", "CreatedAt", "FullName", "IsActive", "IsBlocked", "LastLoginAt", "LastPasswordUpdateAt", "Name", "Password", "UpdatedAt", "Username" },
                values: new object[] { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator", true, false, null, null, "Admin", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
