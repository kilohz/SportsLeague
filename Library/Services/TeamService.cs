using Library.Data;
using Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
	public class TeamService
	{
		private DataRepository<Team> DataRepo;
		public TeamService(DataRepository<Team> repo)
		{
			DataRepo = repo;
		}

		public List<Team> GetAll(int pageSize = 1000, int page = 0)
		{
			return DataRepo.Table.Skip(page * pageSize).Take(pageSize).ToList();
		}

		public Team GetById(int id)
		{
			return DataRepo.Table.FirstOrDefault(p => p.Id == id);
		}


		public void Insert(Team Team)
		{
			DataRepo.Insert(Team);
		}

		public void Update(Team Team)
		{
			DataRepo.Update(Team);
		}

		public void Delete(Team Team)
		{
			DataRepo.Delete(Team);
		}
	}
}
