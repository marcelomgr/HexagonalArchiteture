﻿using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.SqlServer.Person
{
    public class PersonConfiguration : IEntityTypeConfiguration<Entities.Person>
    {
        public void Configure(EntityTypeBuilder<Entities.Person> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
