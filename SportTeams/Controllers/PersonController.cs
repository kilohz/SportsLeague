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
	public class PersonController : ControllerBase
	{
		private readonly ILogger<PersonController> _logger;
		private PersonService _personService;

		public PersonController(ILogger<PersonController> logger,
			PersonService personService)
		{
			_logger = logger;
			_personService = personService;
		}

		[HttpGet]
		public IEnumerable<Person> Get()
		{
			return _personService.GetAll();
		}

		[Route("{id?}")]
		[HttpGet]
		public Person Get(int id)
		{
			return _personService.GetById(id);
		}

		[HttpPost]
		public Object Post([FromBody]Person person)
		{
			try
			{
				_personService.Insert(person);

				return new { success=true, msg="Created Player." };
			}
			catch (Exception ex)
			{
				return new { success = false, msg = $"error: {ex.Message}" };
			}
		}

		[HttpPut]
		public Object Put([FromBody]Person person)
		{
			try
			{
				_personService.Update(person);

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
				var person = _personService.GetById(id);
				_personService.Delete(person);

				return new { success = true, msg = "Created Player." };
			}
			catch (Exception ex)
			{
				return new { success = false, msg = $"error: {ex.Message}" };
			}
		}
	}
}
