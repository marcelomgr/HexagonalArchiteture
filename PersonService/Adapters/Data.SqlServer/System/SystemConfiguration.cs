using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.SqlServer.System
{
    internal class SystemConfiguration : IEntityTypeConfiguration<Entities.System>
    {
        public void Configure(EntityTypeBuilder<Entities.System> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
