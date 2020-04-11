using System;
using System.Threading.Tasks;
using Library.Models.Auth;
using Library.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportTeams.Controllers
{
	[Route("api/auth")]
	[AllowAnonymous]
	public class ApplicationUserController : ControllerBase
	{
		private IAuthenticationService _authService;

		public ApplicationUserController(IAuthenticationService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		[Route("Login")]
		//POST: api/ApplicationUser/Login
		public async Task<ActionResult> Login([FromBody] UserModel model)
		{
			string token;
			var result = _authService.Login(model.Email, model.Password,out token);
			if (result)
			{
				var user = _authService.GetUserByUsername(model.Email);
				var roles = _authService.GetUserRoles(user);

				return Ok(new
				{
					result = new
					{
						Succeeded = true,
						Errors = new String[] { },
						user = new UserModel
						{
							Username = user.UserName,
							Email = user.Email,
							FullName = user.FullName,
							Token = token,
							Roles = roles
						}
					}
				});
			}
			else
			{
				return Ok(new { result = new { Succeeded = false, Errors = new String[] { "IncorrectDetails" } } });
			}
		}

		[HttpPost]
		[Route("Register")]
		//POST: api/user/Register
		public async Task<Object> Register([FromBody] UserModel model)
		{
			try
			{
				var result = _authService.Register(model);
				return Ok(new { result = result });
			}
			catch (Exception e)
			{
				return Ok(new { result = new { Succeeded = false, Errors = new String[] { $"{e?.Message}" } } });
			}
		}

		[HttpPost]
		[Route("VerifyAccount")]
		public async Task<Object> VerifyAccount([FromBody] VerificationModel model)
		{
			try
			{
				_authService.VerifyAccount(model);
				return Ok(true);
			}
			catch (Exception e)
			{
				return Ok(false);
			}
		}

		[HttpGet]
		[Route("ForgotPassword")]
		public async Task<Object> SendForgotPassword(String email)
		{
			try
			{
				_authService.SendForgotPassword(email);

				return Ok(new { result = new { Succeeded = true } });
			}
			catch (Exception e)
			{
				return Ok(new { result = new { Succeeded = false, Errors = new String[] { "UnknownError" } } });
			}
		}

		[HttpPost]
		[Route("ResetPassword")]
		public async Task<Object> ResetPassword([FromBody]PasswordResetModel model)
		{
			try
			{
				_authService.ResetPassword(model);

				return Ok(new { result = new { Succeeded = true } });
			}
			catch (Exception e)
			{
				return Ok(new { result = new { Succeeded = false, Errors = new String[] { "UnknownError" } } });
			}
		}

		[HttpPost]
		[Route("Logout")]
		public async Task<IActionResult> Logout()
		{
			_authService.Logout();
			return Ok(new { result = new { Succeeded = true } });
		}


	}
}