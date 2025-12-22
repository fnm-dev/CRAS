using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CrasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessGroupsAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessGroupId",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "access_group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ParentPermissionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permission_permission_ParentPermissionId",
                        column: x => x.ParentPermissionId,
                        principalTable: "permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "access_group_permission",
                columns: table => new
                {
                    AccessGroupId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_group_permission", x => new { x.AccessGroupId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_access_group_permission_access_group_AccessGroupId",
                        column: x => x.AccessGroupId,
                        principalTable: "access_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_access_group_permission_permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_AccessGroupId",
                table: "users",
                column: "AccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_access_group_permission_PermissionId",
                table: "access_group_permission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_permission_ParentPermissionId",
                table: "permission",
                column: "ParentPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_access_group_AccessGroupId",
                table: "users",
                column: "AccessGroupId",
                principalTable: "access_group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_access_group_AccessGroupId",
                table: "users");

            migrationBuilder.DropTable(
                name: "access_group_permission");

            migrationBuilder.DropTable(
                name: "access_group");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropIndex(
                name: "IX_users_AccessGroupId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "AccessGroupId",
                table: "users");
        }
    }
}
