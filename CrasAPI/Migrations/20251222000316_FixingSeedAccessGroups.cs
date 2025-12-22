using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrasAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixingSeedAccessGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "access_group",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Common user's group");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "access_group",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Common user group");
        }
    }
}
