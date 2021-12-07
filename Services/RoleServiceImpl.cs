using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NETAPI_SEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Services
{
    public class RoleServiceImpl: RoleService
    {
        private readonly ProjectSem3DBContext db;

        public RoleServiceImpl(ProjectSem3DBContext db)
        {
            this.db = db;
        }

        public IEnumerable<IdentityRole> GetAllRoll()
        {
            return db.Roles.ToList();
        }

        public IdentityRole GetRollByID(string roleId)
        {
            return db.Roles.Find(roleId);
        }
    }
}
