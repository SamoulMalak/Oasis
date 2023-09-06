

namespace Oasis.Data.IPersistance
{
    public interface IDbFactory
    {
        DataContext Init();
    }
}
