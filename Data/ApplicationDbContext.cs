using Microsoft.EntityFrameworkCore;

namespace MyApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet properties for each entity type you want to manage.
        public DbSet<Department> Departments { get; set; }
        // Add more DbSet properties for other entities, such as Employees, Projects, etc.
    }
}
