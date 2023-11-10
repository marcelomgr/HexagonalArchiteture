using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer.PersonGender
{
    public class PersonGenderConfiguration : IEntityTypeConfiguration<Entities.PersonGender>
    {
        public void Configure(EntityTypeBuilder<Entities.PersonGender> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}