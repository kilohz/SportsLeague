using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Maps
{
	public class PersonMap : IEntityTypeConfiguration<Person>
	{
		public PersonMap()
		{
		}

		public void Configure(EntityTypeBuilder<Person> builder)
		{
			builder.ToTable("Person");

			builder.HasKey(a => a.Id);
			builder.Property(a => a.CreatedOn).IsRequired();
			builder.Property(a => a.Name).IsRequired().HasMaxLength(128);
			builder.Property(a => a.Email).IsRequired().HasMaxLength(512);
		}
	}
}
