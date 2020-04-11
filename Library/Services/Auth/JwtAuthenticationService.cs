using Library.Data.Entities.Auth;
using Library.Data.Models;
using Library.Models;
using Library.Models.Auth;
using Library.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Library.Services.Auth
{
	public class JwtAuthenticationService : IAuthenticationService
	{
		private UserManager<ApplicationUser> _userManager;
		private SignInManager<ApplicationUser> _signInManager;
		private readonly IHttpContextAccessor _httpcontext;
		protected IEmailService _emailService;
		private ApplicationSettings _appSettings;

		public JwtAuthenticationService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext,
			IOptions<ApplicationSettings> appSettings, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_httpcontext = httpContext;
			_appSettings = appSettings.Value;
		}

		public ApplicationUser GetUser()
		{
			try
			{
				var auth = _httpcontext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
				if (@String.IsNullOrEmpty(auth))
				{
					var token = auth.Split(' ')[1];
					var tokenHandler = new JwtSecurityTokenHandler();
					var jwtToken = tokenHandler.ReadJwtToken(token);
					var userId = Int32.Parse(jwtToken.Payload["UserID"].ToString());
					var user = _userManager.Users.Where(u => u.Id == userId).FirstOrDefault();

					return user;
				}
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		public ApplicationUser GetUserByUsername(string username)
		{
			return _userManager.Users.Where(u => u.UserName == username).FirstOrDefault();
		}

		public bool Login(string username, string password, out string token, bool remember = true)
		{
			token = "";
			var user = _userManager.FindByEmailAsync(username);

			if (user == null) return false;
			if (user == null) return false;
			//if required email verification
			//if (!_userManager.IsEmailConfirmedAsync(user).Result)return false;

			var result = _signInManager.PasswordSignInAsync(username, password, remember, lockoutOnFailure: true).Result;
			if (user != null && result.Succeeded)
			{

				var tokenDescription = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
						new Claim("UserID", user.Id.ToString())
					}),
					Expires = DateTime.UtcNow.AddDays(1),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
																SecurityAlgorithms.HmacSha256Signature)
				};

				var tokenHandler = new JwtSecurityTokenHandler();
				token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescription));
				return true;
			}
			return false;
		}

		public string[] GetUserRoles(ApplicationUser user)
		{
			var roles = _userManager.GetRolesAsync(user).Result;
			return roles.ToArray();
		}

		public IdentityResult Register(UserModel model)
		{
			var user = new ApplicationUser()
			{
				UserName = model.Email,
				Email = model.Email,
				FullName = model.FullName
			};

			var result = _userManager.CreateAsync(user, model.Password).Result;

			//Sends verification email
			if (result.Succeeded)
			{
				//var token = _userManager.GenerateEmailConfirmationTokenAsync(user);

				//TODO: send email
				//_emailService.SendEmail();
			}
			return result;
		}

		public void ResetPassword(PasswordResetModel model)
		{
			var user = _userManager.FindByEmailAsync(model.Email).Result;
			var result = _userManager.ResetPasswordAsync(user, model.Token, model.Password).Result;
		}

		public void SendForgotPassword(string email)
		{
			var user = _userManager.FindByEmailAsync(email).Result;
			var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;

			//TODO: send email
			//_emailService.SendEmail();
		}

		public void VerifyAccount(VerificationModel model)
		{
			var user = _userManager.FindByIdAsync(model.Id).Result;
			var result = _userManager.ConfirmEmailAsync(user, model.Token).Result;
		}

		public string GetTenantName()
		{
			var user = GetUser();
			if (user != null && user.Email == "john3@warpdevelopment.com")
			{
				return "Tenant2";
			}
			else return "";
		}

		public void Logout()
		{
			_signInManager.SignOutAsync();
		}
	}
}
