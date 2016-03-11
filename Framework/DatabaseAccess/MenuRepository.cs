using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
         public MenuRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
