using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Maps
{
	public class MemberMap : IEntityTypeConfiguration<Member>
	{
		public MemberMap()
		{
		}

		public void Configure(EntityTypeBuilder<Member> builder)
		{
			builder.ToTable("Member");

			builder.HasOne(a => a.Person).WithMany(a => a.Members);
			builder.HasOne(a => a.Team).WithMany(a => a.Members);

			builder.HasKey(a => new { a.PersonId, a.TeamId });
		}
	}
}
