using Library.Data;
using Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
	public class PersonService
	{
		private DataRepository<Person> DataRepo;
		public PersonService(DatabaseContext context)
		{
			DataRepo = new DataRepository<Person>(context);
		}

		public List<Person> GetAll(int pageSize = 1000, int page = 0)
		{
			return DataRepo.Table.Skip(page * pageSize).Take(pageSize).ToList();
		}

		public Person GetById(int id)
		{
			return DataRepo.Table.FirstOrDefault(p => p.Id == id);
		}


		public void Insert(Person person)
		{
			DataRepo.Insert(person);
		}

		public void Update(Person person)
		{
			DataRepo.Update(person);
		}

		public void Delete(Person person)
		{
			DataRepo.Delete(person);
		}
	}
}
