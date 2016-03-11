using System.Linq;
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

        public User GetById(int id)
        {
            return Table.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return Table.FirstOrDefault(x => x.IsDeleted == false && x.Email == email);
        }
    }
}
