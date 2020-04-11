using System;
using Microsoft.AspNetCore.Identity;

namespace Library.Data.Entities.Auth
{
	public class ApplicationUser : IdentityUser<Int32>
	{
		public String FullName { get; set; }
	}
}
