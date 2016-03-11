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
    }
}
