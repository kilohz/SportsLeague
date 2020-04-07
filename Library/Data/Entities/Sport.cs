using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Entities
{
	public class Sport : BaseEntity
	{
		public Sport()
		{
			this.CreatedOn = DateTimeOffset.Now;
		}
		public int Id { get; set; }
		public DateTimeOffset CreatedOn { get; set; }
		public string Name { get; set; }
	}
}
