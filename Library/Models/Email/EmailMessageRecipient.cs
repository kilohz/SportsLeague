using System;
using Library.Data.Entities;

namespace Library.Models.Email
{
	public class EmailMessageRecipient : BaseEntity
	{
		public Int32 EmailMessageId { get; set; }
		public Nullable<DateTime> SentOn { get; set; }
		public String Title { get; set; }
		public String Name { get; set; }
		public String Surname { get; set; }
		public String EmailAddress { get; set; }
		public String Status { get; set; }

		#region Navigation properties
		public virtual EmailMessage EmailMessage { get; set; }
		#endregion
	}
}