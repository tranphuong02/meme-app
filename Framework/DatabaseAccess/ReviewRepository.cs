using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace DatabaseAccess
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
         public ReviewRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
