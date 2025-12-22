using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrasAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedAccessGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "access_group",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator's group", "Administrator" },
                    { 2, "Common user's group", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "access_group",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "access_group",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
