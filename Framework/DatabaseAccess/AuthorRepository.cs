using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
         public AuthorRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
