using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Models
{
	public class EmailSettings
	{
		public String ReplyAddress { get; set; }
		public String FromAddress { get; set; }
		public String FromName { get; set; }
		public String Port { get; set; }
		public String Host { get; set; }
		public String User { get; set; }
		public String Password { get; set; }
	}
}
