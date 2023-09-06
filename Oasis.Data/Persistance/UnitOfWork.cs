using Oasis.Data.IPersistance;


namespace Oasis.Data.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IDbFactory _dbFactory;
        private DataContext _dbcontext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
            this._dbcontext = this._dbcontext ?? (this._dbcontext = dbFactory.Init());

        }
        public DataContext DbContext
        {
            get { return _dbcontext ?? (_dbcontext = _dbFactory.Init()); }

        }

        public void SaveChanges()
        {
            this._dbcontext.SaveChanges();
        }
    }
}
