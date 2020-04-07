using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Entities
{
	public class Member : BaseEntity
	{
		public int PersonId { get; set; }
		public int TeamId { get; set; }

		#region Navigational Properties
		public Person Person { get; set; }
		public Team Team { get; set; }

		#endregion
	}
}
