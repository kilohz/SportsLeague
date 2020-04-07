using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Entities
{
	public class League : BaseEntity
	{
		public League()
		{
			this.CreatedOn = DateTimeOffset.Now;
		}

		public int Id { get; set; }
		public DateTimeOffset CreatedOn { get; set; }

		public string Name { get; set; }

		#region Navigational Properties
		public List<Score> Scores { get; set; }

		#endregion
	}
}
