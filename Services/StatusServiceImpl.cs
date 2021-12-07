using NETAPI_SEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Services
{
	public class StatusServiceImpl : StatusService
	{
		private readonly ProjectSem3DBContext _db;

		public StatusServiceImpl(ProjectSem3DBContext db)
		{
			this._db = db;
		}

		public IEnumerable<Status> GetAllStatus()
		{
			return _db.Statuses.Select(s => new Status {
				Name = s.Name,
				StatusId = s.StatusId
			}).ToList();
		}
	}
}
