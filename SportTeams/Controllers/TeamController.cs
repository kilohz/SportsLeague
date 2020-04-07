using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data.Entities;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SportTeams.Controllers
{
	[Route("api/[controller]")]
	public class TeamController : ControllerBase
	{
		private readonly ILogger<TeamController> _logger;
		private TeamService _TeamService;

		public TeamController(ILogger<TeamController> logger,
			TeamService TeamService)
		{
			_logger = logger;
			_TeamService = TeamService;
		}

		[HttpGet]
		public IEnumerable<Team> Get()
		{
			return _TeamService.GetAll();
		}

		[Route("{id?}")]
		[HttpGet]
		public Team Get(int id)
		{
			return _TeamService.GetById(id);
		}

		[HttpPost]
		public Object Post([FromBody]Team Team)
		{
			try
			{
				_TeamService.Insert(Team);

				return new { success=true, msg="Created Player." };
			}
			catch (Exception ex)
			{
				return new { success = false, msg = $"error: {ex.Message}" };
			}
		}

		[HttpPut]
		public Object Put([FromBody]Team Team)
		{
			try
			{
				_TeamService.Update(Team);

				return new { success = true, msg = "Created Player." };
			}
			catch (Exception ex)
			{
				return new { success = false, msg = $"error: {ex.Message}" };
			}
		}

		[Route("{id?}")]
		[HttpDelete]
		public Object Delete(int id)
		{
			try
			{
				var Team = _TeamService.GetById(id);
				_TeamService.Delete(Team);

				return new { success = true, msg = "Created Player." };
			}
			catch (Exception ex)
			{
				return new { success = false, msg = $"error: {ex.Message}" };
			}
		}
	}
}
