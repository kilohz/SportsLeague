using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Maps
{
	public class LeagueMap : IEntityTypeConfiguration<League>
	{
		public LeagueMap()
		{
		}

		public void Configure(EntityTypeBuilder<League> builder)
		{
			builder.ToTable("League");

			builder.HasKey(a => a.Id);

			builder.Property(a => a.CreatedOn).IsRequired();
			builder.Property(a => a.Name).IsRequired().HasMaxLength(128);
		}
	}
}
