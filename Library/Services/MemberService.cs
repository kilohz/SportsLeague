using Library.Data;
using Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
	public class MemberService
	{
		private DataRepository<Member> DataRepo;
		private DataRepository<Person> PersonRepo;

		public MemberService(DataRepository<Member> memberRepo, DataRepository<Person> personRepo)
		{
			DataRepo = memberRepo;
			PersonRepo = personRepo;
		}

		public List<Person> GetAll(int id, int pageSize = 1000, int page = 0)
		{
			return (from p in PersonRepo.Table
					join m in DataRepo.Table on p.Id equals m.PersonId
					where m.TeamId == id
					select p
				   ).Skip(page * pageSize).Take(pageSize).ToList();
		}

		public Member GetById(int personId, int teamId)
		{
			return DataRepo.Table.FirstOrDefault(p => p.PersonId == personId && p.TeamId == teamId);
		}

		public void Insert(Member Member)
		{
			DataRepo.Insert(Member);
		}

		public void Update(Member Member)
		{
			DataRepo.Update(Member);
		}

		public void Delete(Member Member)
		{
			DataRepo.Delete(Member);
		}
	}
}
