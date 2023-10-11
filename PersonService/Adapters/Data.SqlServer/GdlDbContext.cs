using Data.SqlServer.Person;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;

namespace Data.SqlServer
{
    public class GdlDbContext : DbContext
    {
        public GdlDbContext(DbContextOptions<GdlDbContext> options) : base(options) { }

        public virtual DbSet<Entities.Person> Persons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
