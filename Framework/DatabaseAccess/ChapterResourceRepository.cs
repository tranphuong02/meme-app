using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class ChapterResourceRepository : Repository<Chapter_Resource>, IChapterResourceRepository
    {
         public ChapterResourceRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
