using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Maps
{
	public class SportMap : IEntityTypeConfiguration<Sport>
	{
		public SportMap()
		{
		}

		public void Configure(EntityTypeBuilder<Sport> builder)
		{
			builder.ToTable("Sport");

			builder.HasKey(a => a.Id);

			builder.Property(a => a.CreatedOn).IsRequired();
			builder.Property(a => a.Name).IsRequired().HasMaxLength(128);
		}
	}
}
