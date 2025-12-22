using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrasAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixingDefaultAdminUserPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$12$Pe/.UaVMhnGgmL8i/6dxJ.nDeuokmeA9aFuvND816gYYw8bFmk2Ee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "Admin");
        }
    }
}
