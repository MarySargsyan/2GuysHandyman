using Microsoft.EntityFrameworkCore;

namespace _2GuysHandyman.models
{
    public class dbContext: DbContext
    {
        public dbContext(DbContextOptions<dbContext> options):base(options)
        {

        }

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderStatuses> OrderStatuses { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Services> Services { get; set; }
    }
}
