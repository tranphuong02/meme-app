using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
         public ChapterRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
