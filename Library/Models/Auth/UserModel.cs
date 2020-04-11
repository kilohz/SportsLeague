using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Auth
{
	public class UserModel
	{
		public String Username { get; set; }
		public String FullName { get; set; }
		public String Email { get; set; }
		public String Password { get; set; }
		public String Token { get; set; }
		public IList<String> Roles { get; set; }
	}
}
