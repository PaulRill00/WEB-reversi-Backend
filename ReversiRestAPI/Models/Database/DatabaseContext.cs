using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using ReversiRestApi.Models;
using ReversiRestAPI.Models.DbModels;

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
