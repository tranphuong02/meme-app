using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Core.DI;
using Core.Model.Entities;

namespace Core.Model
{
    public interface IDbContext : IPerRequestDependency, IDisposable
    {
        IDbSet<T> Set<T>() where T : BaseEntity;

        DbEntityEntry<T> EntityEntry<T>(T entity) where T : BaseEntity;

        int Commit();

        Database Db { get; }
    }
}
