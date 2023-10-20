using Data.SqlServer.Person;
using Data.SqlServer.PersonType;
using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer
{
    public class GdlDbContext : DbContext
    {
        public GdlDbContext(DbContextOptions<GdlDbContext> options) : base(options) { }

        public virtual DbSet<Entities.Person> Persons { get; set; }
        public virtual DbSet<Entities.PersonType> PersonTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());
        }
    }
}
