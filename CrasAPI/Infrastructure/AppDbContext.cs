using CrasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrasAPI.Infrastructure

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessGroupPermission>()
                .HasKey(x => new { x.AccessGroupId, x.PermissionId });

            modelBuilder.Entity<AccessGroupPermission>()
                .HasOne(x => x.AccessGroup)
                .WithMany(g => g.Permissions)
                .HasForeignKey(x => x.AccessGroupId);

            modelBuilder.Entity<AccessGroupPermission>()
                .HasOne(x => x.Permission)
                .WithMany()
                .HasForeignKey(x => x.PermissionId);

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Code = "user_view", Description = "Permission to view users" },
                new Permission { Id = 2, Code = "user_create", Description = "Permission to create users" },
                new Permission { Id = 3, Code = "user_edit", Description = "Permission to edit users" },
                new Permission { Id = 4, Code = "user_delete", Description = "Permission to delete users" },
                new Permission { Id = 5, Code = "accessgroup_view", Description = "Permission to view access groups" },
                new Permission { Id = 6, Code = "accessgroup_create", Description = "Permission to create access groups" },
                new Permission { Id = 7, Code = "accessgroup_edit", Description = "Permission to edit access groups" },
                new Permission { Id = 8, Code = "accessgroup_delete", Description = "Permission to delete access groups" }
            );
            
            modelBuilder.Entity<AccessGroup>().HasData(
                new AccessGroup { Id = 1, Name = "Administrator", Description = "Administrator's group" },
                new AccessGroup { Id = 2, Name = "User", Description = "Common user's group" }
            );

            modelBuilder.Entity<AccessGroupPermission>().HasData(
                new AccessGroupPermission { AccessGroupId = 1, PermissionId = 1 },
                new AccessGroupPermission { AccessGroupId = 1, PermissionId = 2 },
                new AccessGroupPermission { AccessGroupId = 1, PermissionId = 3 },
                new AccessGroupPermission { AccessGroupId = 1, PermissionId = 4 },
                new AccessGroupPermission { AccessGroupId = 1, PermissionId = 5 },
                new AccessGroupPermission { AccessGroupId = 1, PermissionId = 6 },
                new AccessGroupPermission { AccessGroupId = 1, PermissionId = 7 },
                new AccessGroupPermission { AccessGroupId = 1, PermissionId = 8 }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    FullName = "Administrator",
                    Username = "admin@admin.com",
                    Password = "$2a$12$Pe/.UaVMhnGgmL8i/6dxJ.nDeuokmeA9aFuvND816gYYw8bFmk2Ee",
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true,
                    IsBlocked = false,
                    AccessGroupId = 1
                }
            );
        }

        public DbSet<User> Users { get; set; }
    }
}
