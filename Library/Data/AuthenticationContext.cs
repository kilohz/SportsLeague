using System;
using Library.Data.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
	public class AuthentificationContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
	{
		public AuthentificationContext(DbContextOptions<AuthentificationContext> options) : base(options)
		{
		}

		public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
	}
}
