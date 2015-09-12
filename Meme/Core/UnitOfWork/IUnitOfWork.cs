using Core.DI;
using Core.Model;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork : IDependency
    {
        int Commit();

        IDbContext DbContext { get; }
    }
}
