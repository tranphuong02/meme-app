using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class ConfigRepository : Repository<Config>, IConfigRepository
    {
         public ConfigRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
