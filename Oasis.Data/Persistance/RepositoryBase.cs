using Microsoft.EntityFrameworkCore;
using Oasis.Data.IPersistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.Data.Persistance
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private DataContext dataContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        protected DataContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }
        #endregion

        #region Implementation

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public virtual T Create(T entity)
        {
            dbSet.Add(entity);
            return dbSet.FirstOrDefault<T>();
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
                dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Update(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                dbSet.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                dbSet.Remove(entity);
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return dbSet.AsNoTracking();
            }
        }

        public T GetByIdRelatedTable(object id, params Expression<Func<T, object>>[] includeProperties)
        {

            foreach (var includeProperty in includeProperties)
            {
                dbSet.Include(includeProperty);
            }
            return dbSet.Find(id);
        }
        #endregion
    }
}
