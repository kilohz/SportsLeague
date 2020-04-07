using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Maps
{
	public class TeamMap : IEntityTypeConfiguration<Team>
	{
		public TeamMap()
		{
		}

		public void Configure(EntityTypeBuilder<Team> builder)
		{
			builder.ToTable("Team");

			builder.HasKey(a => a.Id);

			builder.Property(a => a.CreatedOn).IsRequired();
			builder.Property(a => a.Name).IsRequired().HasMaxLength(128);
		}
	}
}
