using AirlineDll.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirlineDll
{
    public class AirlineDbContext :DbContext
    {
        public DbSet<Airline>? Airlines { set; get; }
        public AirlineDbContext()
        {
        }
        public AirlineDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-00LQG0A;Database=AirLineAssgnment;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>().HasIndex(airline => airline.Name).IsUnique();
        }
    }
}
