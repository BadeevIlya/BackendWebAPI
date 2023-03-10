using Microsoft.EntityFrameworkCore;

namespace Badeev_L01.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Film> Films { get; set; }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
