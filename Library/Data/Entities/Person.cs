using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Entities
{
	public class Person : BaseEntity
	{
		public Person()
		{
			this.CreatedOn = DateTimeOffset.Now;
		}

		public int Id { get; set; }
		public DateTimeOffset CreatedOn { get; set; }
		public string Name { get; set; }

		public string Email { get; set; }

		#region Navigational Properties
		public List<Score> Scores { get; set; }
		public List<Member> Members { get; set; }

		#endregion
	}
}
