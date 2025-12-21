using Microsoft.EntityFrameworkCore;
using CrasAPI.Model;

namespace CrasAPI.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
