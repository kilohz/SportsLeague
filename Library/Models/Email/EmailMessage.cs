using System;
using System.Collections.Generic;

namespace Library.Models.Email
{
	public class EmailMessage
	{
		public EmailMessage() => this.EmailMessageRecipients = new HashSet<EmailMessageRecipient>();

		public DateTime CreatedOn { get; set; }
		public String Subject { get; set; }
		public String FromName { get; set; }
		public String FromAddress { get; set; }
		public String ReplyAddress { get; set; }
		public String MessageBody { get; set; }
		public Dictionary<String, String> Parameters { get; set; }

		#region Navigation properties
		public virtual ICollection<EmailMessageRecipient> EmailMessageRecipients { get; set; }
		#endregion
	}
	public class EmailMessageAttachment
	{
		public Byte[] Attachment { get; set; }
		public String AttachmentName { get; set; }
	}
}