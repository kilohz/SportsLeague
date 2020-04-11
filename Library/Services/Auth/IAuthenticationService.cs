using Library.Data.Entities.Auth;
using Library.Models;
using Library.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Auth
{
	public interface IAuthenticationService
	{
		ApplicationUser GetUser();

		ApplicationUser GetUserByUsername(string username);

		string GetTenantName();

		bool Login(string username, string password, out string token, bool remember = true);

		IdentityResult Register(UserModel model);

		void VerifyAccount(VerificationModel model);

		void SendForgotPassword(string email);

		void ResetPassword(PasswordResetModel model);

		string[] GetUserRoles(ApplicationUser user);

		void Logout();
	}
}
