using Oasis.Data.IPersistance;


namespace Oasis.Data.Persistance
{
    public class DbFactory : Disposable, IDbFactory
    {
        DataContext dbContext;
        public DataContext Init()
        {
            return dbContext ?? (dbContext = new DataContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
