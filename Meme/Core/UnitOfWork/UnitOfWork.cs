using Core.Model;

namespace Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbContext DbContext { get; private set; }

        public UnitOfWork(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public int Commit()
        {
            return DbContext.Commit();
        }
    }
}
