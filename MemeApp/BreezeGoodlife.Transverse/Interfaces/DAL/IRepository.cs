//////////////////////////////////////////////////////////////////////
// File Name    : IRepository
// Summary      :
// Author       : dinh.nguyen
// Change Log   : 11/12/2015 4:24:50 PM - Create Date
/////////////////////////////////////////////////////////////////////

using Framework.DataAccess.Contracts.Interfaces;
using Framework.DI.Contracts.Interfaces;

namespace Transverse.Interfaces.DAL
{
    public interface IRepository<TEntity> : IBaseRepository<TEntity>, IDependency where TEntity : class
    {
    }
}