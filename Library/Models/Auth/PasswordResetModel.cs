using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Auth
{
	public class PasswordResetModel
	{
		public String Password { get; set; }
		public String Email { get; set; }
		public String Token { get; set; }
	}
}
