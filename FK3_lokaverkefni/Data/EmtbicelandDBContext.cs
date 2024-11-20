using FK3_lokaverkefni.Models;
using Microsoft.EntityFrameworkCore;
using Route = FK3_lokaverkefni.Models.Route;

namespace FK3_lokaverkefni.Data
{
    public class EmtbicelandDBContext : DbContext
    {
        public DbSet<Event>? Events { get; set; }
        public DbSet<Route>? Routes { get; set; }
        public DbSet<User>? Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EmtbicelandDB");
        }
    }
}
