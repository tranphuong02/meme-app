using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Core.Model.Entities;
using Core.UnitOfWork;
using STS.DAL.Repository;

namespace Core.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private IDbSet<T> _entity;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T GetById(int id)
        {
            return Table.FirstOrDefault(e => e.Id == id);
        }

        public void Insert(T entity, bool isActive = true)
        {
            SetDefaultFieldsWhenCreate(entity, isActive);
            Entity.Add(entity);
        }

        public void Insert(IEnumerable<T> entities, bool isActive = true)
        {
            foreach (var entity in entities)
            {
                SetDefaultFieldsWhenCreate(entity, isActive);
                Insert(entity);
            }
        }

        public void Update(T entity)
        {
            Entity.Attach(entity);
            _unitOfWork.DbContext.EntityEntry(entity).State = EntityState.Modified;
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public void Delete(T entity, bool isRemoveForever = false)
        {
            if (isRemoveForever)
            {
                try
                {
                    Entity.Remove(entity);
                }
                catch
                {
                    ResetDeleteState(entity);
                    throw;
                }
            }
            else
            {
                entity.IsDelete = true;
                Update(entity);
            }
        }

        public void Delete(IEnumerable<T> entities, bool isRemoveForever = false)
        {
            foreach (var entity in new List<T>(entities))
            {
                Delete(entity, isRemoveForever);
            }
        }

        public void Delete(int entityId, bool isRemoveForever = false)
        {
            var entity = GetById(entityId);
            Delete(entity, isRemoveForever);
        }

        public void Delete(IEnumerable<int> entityIds, bool isRemoveForever = false)
        {
            foreach (var entityId in entityIds)
            {
                Delete(entityId, isRemoveForever);
            }
        }

        public int Count(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             bool isActive = false, bool includeDelete = false,
            string includeProperties = "")
        {
            var query = Table;

            query = isActive ? query.Where(e => e.IsActive || !e.IsActive) : query.Where(e => e.IsActive);
            query = includeDelete ? query.Where(e => e.IsDelete || !e.IsDelete) : query.Where(e => !e.IsDelete);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.Count();
        }

        public bool Any(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             bool isActive = false, bool includeDelete = false,
            string includeProperties = "")
        {
            var query = Table;

            query = isActive ? query.Where(e => e.IsActive || !e.IsActive) : query.Where(e => e.IsActive);
            query = includeDelete ? query.Where(e => e.IsActive || !e.IsActive) : query.Where(e => !e.IsActive);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.Any();
        }

        public int CountAll()
        {
            return Table.Count();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool isActive = false, bool includeDelete = false,
            string includeProperties = "")
        {
            var query = Table;

            query = isActive ? query.Where(e => e.IsActive || !e.IsActive) : query.Where(e => e.IsActive);
            query = includeDelete ? query.Where(e => e.IsDelete || !e.IsDelete) : query.Where(e => !e.IsDelete);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query) : query;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool isActive = false, bool includeDelete = false,
            string includeProperties = "")
        {
            var query = Table;

            query = isActive ? query.Where(e => e.IsActive || !e.IsActive) : query.Where(e => e.IsActive);
            query = includeDelete ? query.Where(e => e.IsDelete || !e.IsDelete) : query.Where(e => !e.IsDelete);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool includeUnPublished = false, bool includeDelete = false,
            string includeProperties = "")
        {
            return Get(filter, orderBy, includeUnPublished, includeDelete, includeProperties);
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entity.Where(e => !e.IsDelete);
            }
        }

        /// <summary>
        /// Non-query commands can be sent to the database using the ExecuteSqlCommand method on Database.
        /// </summary>
        /// <param name="sqlCommand"></param>
        public void ExecuteSqlCommand(string sqlCommand)
        {
            _unitOfWork.DbContext.Db.ExecuteSqlCommand(sqlCommand);
        }

        /// <summary>
        /// A SQL query returning instances of any type, including primitive types, can be created using the SqlQuery method on the Database class.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public DbRawSqlQuery<T> SqlQuery(string sqlQuery)
        {
            return _unitOfWork.DbContext.Db.SqlQuery<T>(sqlQuery);
        }

        private IDbSet<T> Entity
        {
            get { return _entity ?? (_entity = _unitOfWork.DbContext.Set<T>()); }
        }

        #region Helpers

        protected virtual void SetDefaultFieldsWhenCreate(T entity, bool isActive = true)
        {
            entity.CreatedDate = DateTime.Now;
            entity.IsActive = isActive;
        }

        public void ResetDeleteState(T entityToDelete)
        {
            _unitOfWork.DbContext.EntityEntry(entityToDelete).Reload();
        }

        #endregion

    }
}
