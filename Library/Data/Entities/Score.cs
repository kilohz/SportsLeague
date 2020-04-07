using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Entities
{
	public class Score : BaseEntity
	{
		public Score()
		{
			this.CreatedOn = DateTimeOffset.Now;
		}
		public int Id { get; set; }

		public int SportId { get; set; }

		public int? PersonId { get; set; }

		public int? LeagueId { get; set; }

		public int? TeamId { get; set; }
		public DateTimeOffset CreatedOn { get; set; }

		public int For { get; set; }

		public int Against { get; set; }

		#region navigational properties

		public Sport Sport { get; set; }

		public Person Person { get; set; }

		public League League { get; set; }

		public Team Team { get; set; }

		#endregion
	}
}
