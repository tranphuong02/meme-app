using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
         public ResourceRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
