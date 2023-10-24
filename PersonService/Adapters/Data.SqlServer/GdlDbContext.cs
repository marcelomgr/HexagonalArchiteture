using Data.SqlServer.Person;
using Data.SqlServer.PersonType;
using Data.SqlServer.PersonAggregate;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;

namespace Data.SqlServer
{
    public class GdlDbContext : DbContext
    {
        public GdlDbContext(DbContextOptions<GdlDbContext> options) : base(options) { }

        public virtual DbSet<Entities.Person> Persons { get; set; }
        public virtual DbSet<Entities.PersonType> PersonTypes { get; set; }
        public virtual DbSet<Entities.PersonAggregate> PersonAggregates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonAggregateConfiguration());
        }
    }
}
