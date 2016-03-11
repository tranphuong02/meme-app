using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class GenreCategoryRepository : Repository<Genre_Category>, IGenreCategoryRepository
    {
         public GenreCategoryRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
