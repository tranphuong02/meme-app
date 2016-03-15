using System.Collections.Generic;
using System.Linq;
using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
         public RoleRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public IList<Role> GetAll()
        {
            return GetAll(x => x.IsDeleted == false).ToList();
        }
    }
}
