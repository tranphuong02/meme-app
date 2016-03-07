//////////////////////////////////////////////////////////////////////
// File Name    : IDatabaseMigrations
// Summary      :
// Author       : dinh.nguyen
// Change Log   : 10/12/2015 2:48:45 PM - Create Date
/////////////////////////////////////////////////////////////////////

using Framework.DI.Contracts.Interfaces;

namespace Transverse.Interfaces.Business
{
    public interface IDatabaseMigrationsBusiness : IDependency
    {
        /// <summary>
        ///     Migration data
        /// </summary>
        void ApplyDatabaseMigrations();
    }
}