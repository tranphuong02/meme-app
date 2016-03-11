using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {
         public UserRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
