using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Email
{
	public class Email
	{
		public EmailMessage Message { get; set; }
		public EmailMessageRecipient Recipients { get; set; }
	}
}
