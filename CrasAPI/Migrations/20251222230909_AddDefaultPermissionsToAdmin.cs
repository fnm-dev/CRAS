using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultPermissionsToAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "permission",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "Id", "Code", "Description", "ParentPermissionId" },
                values: new object[,]
                {
                    { 1, "user_view", "Permission to view users", null },
                    { 2, "user_create", "Permission to create users", null },
                    { 3, "user_edit", "Permission to edit users", null },
                    { 4, "user_delete", "Permission to delete users", null },
                    { 5, "accessgroup_view", "Permission to view access groups", null },
                    { 6, "accessgroup_create", "Permission to create access groups", null },
                    { 7, "accessgroup_edit", "Permission to edit access groups", null },
                    { 8, "accessgroup_delete", "Permission to delete access groups", null }
                });

            migrationBuilder.InsertData(
                table: "access_group_permission",
                columns: new[] { "AccessGroupId", "PermissionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "access_group_permission",
                keyColumns: new[] { "AccessGroupId", "PermissionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "access_group_permission",
                keyColumns: new[] { "AccessGroupId", "PermissionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "access_group_permission",
                keyColumns: new[] { "AccessGroupId", "PermissionId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "access_group_permission",
                keyColumns: new[] { "AccessGroupId", "PermissionId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "access_group_permission",
                keyColumns: new[] { "AccessGroupId", "PermissionId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "access_group_permission",
                keyColumns: new[] { "AccessGroupId", "PermissionId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "access_group_permission",
                keyColumns: new[] { "AccessGroupId", "PermissionId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "access_group_permission",
                keyColumns: new[] { "AccessGroupId", "PermissionId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "permission");
        }
    }
}
