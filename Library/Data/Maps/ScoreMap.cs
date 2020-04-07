using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Maps
{
	public class ScoreMap : IEntityTypeConfiguration<Score>
	{
		public ScoreMap()
		{
		}

		public void Configure(EntityTypeBuilder<Score> builder)
		{
			builder.ToTable("Score");

			builder.HasKey(a => a.Id);

			builder.HasOne(a => a.Person)
				.WithMany(a => a.Scores);

			builder.HasOne(a => a.Team)
				.WithMany(a => a.Scores);

			builder.HasOne(a => a.League)
				.WithMany(a => a.Scores);

			builder.Property(a => a.CreatedOn).IsRequired();
			builder.Property(a => a.For).IsRequired();
			builder.Property(a => a.Against).IsRequired();
		}
	}
}
