using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.Data.IPersistance
{
    public partial interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Get entity using delegate
        /// </summary>
        /// <param name="where">Expression</param>
        /// <returns>Entity</returns>
        T Get(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets entities using delegate
        /// </summary>
        /// <param name="where">Expression</param>
        /// <returns>Entities</returns>
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> GetAll();

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Add(T entity);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity</param>
        T Create(T entity);

        /// <summary>
        /// Add entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Add(IEnumerable<T> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Delete entities with Expression
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }

        T GetByIdRelatedTable(object id, params Expression<Func<T, object>>[] includeProperties);

    }
}
