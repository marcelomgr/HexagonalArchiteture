using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.SqlServer.PersonType
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<Entities.PersonType>
    {
        public void Configure(EntityTypeBuilder<Entities.PersonType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
