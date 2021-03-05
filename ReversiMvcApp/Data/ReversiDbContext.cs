using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Data
{
    public class ReversiDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public ReversiDbContext(DbContextOptions<ReversiDbContext> options)
            : base(options)
        {
        }
    }
}
