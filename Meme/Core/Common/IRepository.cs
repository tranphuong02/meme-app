using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Core.DI;

namespace Core.Common
{
    public interface IRepository<T> : IDependency
    {
        T GetById(int id);

        void Insert(T entity, bool isPublished = true);

        void Insert(IEnumerable<T> entities, bool isActive = true);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(T entity, bool isRemoveForever = false);

        void Delete(IEnumerable<T> entities, bool isRemoveForever = false);

        void Delete(int id, bool isRemoveForever = false);

        void Delete(IEnumerable<int> entityIds, bool isRemoveForever = false);

        int Count(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeActive = false, bool includeDelete = false,
            string includeProperties = "");

        bool Any(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeActive = false, bool includeDelete = false,
            string includeProperties = "");

        int CountAll();

        IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeActive = false, bool includeDelete = false,
            string includeProperties = "");

        T FirstOrDefault(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeActive = false, bool includeDelete = false,
            string includeProperties = "");

        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeActive = false, bool includeDelete = false,
            string includeProperties = "");

        IQueryable<T> Table { get; }

        /// <summary>
        /// Non-query commands
        /// </summary>
        /// <param name="sqlCommand"></param>
        void ExecuteSqlCommand(string sqlCommand);

        /// <summary>
        /// A SQL query returning instances of any T type
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        DbRawSqlQuery<T> SqlQuery(string sqlQuery);
    }
}
