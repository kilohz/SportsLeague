using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data.Entities;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SportMembers.Controllers
{
	[Route("api/[controller]")]
	public class MemberController : ControllerBase
	{
		private readonly ILogger<MemberController> _logger;
		private MemberService _MemberService;

		public MemberController(ILogger<MemberController> logger,
			MemberService MemberService)
		{
			_logger = logger;
			_MemberService = MemberService;
		}

		[Route("{id?}")]
		[HttpGet]
		public IEnumerable<Person> Get(int id)
		{
			return _MemberService.GetAll(id);
		}

		[HttpPost]
		public Object Post([FromBody]Member Member)
		{
			try
			{
				_MemberService.Insert(Member);

				return new { success=true, msg="Created Player." };
			}
			catch (Exception ex)
			{
				return new { success = false, msg = $"error: {ex.Message}" };
			}
		}

		[Route("{id?}")]
		[HttpDelete]
		public Object Delete(int personId, int teamId)
		{
			try
			{
				var Member = _MemberService.GetById(personId, teamId);
				_MemberService.Delete(Member);

				return new { success = true, msg = "Created Player." };
			}
			catch (Exception ex)
			{
				return new { success = false, msg = $"error: {ex.Message}" };
			}
		}
	}
}
