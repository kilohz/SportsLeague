using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Entities
{
	public class Team : BaseEntity
	{
		public Team()
		{
			this.CreatedOn = DateTimeOffset.Now;
		}
		public int Id { get; set; }
		public DateTimeOffset CreatedOn { get; set; }
		public string Name { get; set; }


		#region Navigational Properties
		public List<Score> Scores { get; set; }
		public List<Member> Members { get; set; }
		#endregion
	}
}
