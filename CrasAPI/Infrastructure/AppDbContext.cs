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

            modelBuilder.Entity<AccessGroup>().HasData(
                new AccessGroup { Id = 1, Name = "Administrator", Description = "Administrator's group" },
                new AccessGroup { Id = 2, Name = "User", Description = "Common user's group" }
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
