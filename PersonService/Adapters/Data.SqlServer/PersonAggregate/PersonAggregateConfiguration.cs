using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;

namespace Data.SqlServer.PersonAggregate
{
    public class PersonAggregateConfiguration : IEntityTypeConfiguration<Entities.PersonAggregate>
    {
        public void Configure(EntityTypeBuilder<Entities.PersonAggregate> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
