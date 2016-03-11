using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
         public GenreRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
