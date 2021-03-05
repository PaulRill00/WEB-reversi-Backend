using Microsoft.EntityFrameworkCore;

namespace ReversiRestAPI.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
