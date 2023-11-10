using Data.SqlServer.Person;
using Data.SqlServer.PersonType;
using Entities = Domain.Entities;
using Data.SqlServer.PersonGender;
using Microsoft.EntityFrameworkCore;
using Data.SqlServer.PersonAggregate;

namespace Data.SqlServer
{
    public class GdlDbContext : DbContext
    {
        public GdlDbContext(DbContextOptions<GdlDbContext> options) : base(options) { }

        public virtual DbSet<Entities.Person> Persons { get; set; }
        public virtual DbSet<Entities.PersonType> PersonTypes { get; set; }
        public virtual DbSet<Entities.PersonGender> PersonGenders { get; set; }
        public virtual DbSet<Entities.PersonAggregate> PersonAggregates { get; set; }
        public virtual DbSet<Entities.ChangeLog> ChangeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonGenderConfiguration());
            modelBuilder.ApplyConfiguration(new PersonAggregateConfiguration());
        }
    }
}
