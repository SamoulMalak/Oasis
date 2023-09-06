
using System.Linq.Expressions;


namespace Oasis.Data.IPersistance
{
    public partial interface IRepository<T> where T : class
    {
        
        T GetById(object id);
        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        void Add(T entity);
        T Create(T entity);
        void Add(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Delete(Expression<Func<T, bool>> where);
        IQueryable<T> TableNoTracking { get; }
        T GetByIdRelatedTable(object id, params Expression<Func<T, object>>[] includeProperties);

    }
}
