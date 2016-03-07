//////////////////////////////////////////////////////////////////////
// File Name    : IDatabaseMigrations
// Summary      :
// Author       : dinh.nguyen
// Change Log   : 10/12/2015 2:48:02 PM - Create Date
/////////////////////////////////////////////////////////////////////

using Framework.DI.Contracts.Interfaces;

namespace BreezeGoodlife.Transverse.Interfaces.DAL
{
    public interface IDatabaseMigrationsRepository : IDependency
    {
        /// <summary>
        ///     Migration data
        /// </summary>
        void ApplyDatabaseMigrations();
    }
}