using Microsoft.EntityFrameworkCore;
using PropertiesMinimalAPI.Models;

namespace PropertiesMinimalAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {
        }

        public DbSet<Properties> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Properties>().HasData(
                new Properties { Id = 1, Name = "Casa las palmas 1", Description = "Descripción test 1", IsActive = true, CreatedAt = DateTime.UtcNow, Location = "Test1" },
                new Properties { Id = 2, Name = "Casa las palmas 2", Description = "Descripción test 1", IsActive = true, CreatedAt = DateTime.UtcNow, Location = "Test2" },
                new Properties { Id = 3, Name = "Casa las palmas 3", Description = "Descripción test 1", IsActive = true, CreatedAt = DateTime.UtcNow, Location = "Test3" },
                new Properties { Id = 4, Name = "Casa las palmas 4", Description = "Descripción test 1", IsActive = true, CreatedAt = DateTime.UtcNow, Location = "Test4" },
                new Properties { Id = 5, Name = "Casa las palmas 5", Description = "Descripción test 1", IsActive = true, CreatedAt = DateTime.UtcNow, Location = "Test5" },
                new Properties { Id = 6, Name = "Casa las palmas 6", Description = "Descripción test 1", IsActive = true, CreatedAt = DateTime.UtcNow, Location = "Test6" }
            );
        }
    }
}
