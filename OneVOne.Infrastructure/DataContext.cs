using Microsoft.EntityFrameworkCore;
using OneVOne.Core.Entities;
using System.Reflection;

namespace OneVOne.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PlayByPlayStatistics> PlayByPlayStatistics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PlayerImage> PlayerImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}