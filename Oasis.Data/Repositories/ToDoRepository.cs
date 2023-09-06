using Oasis.Data.Entities;
using Oasis.Data.IPersistance;
using Oasis.Data.IRepositories;
using Oasis.Data.Persistance;


namespace Oasis.Data.Repositories
{
    public class ToDoRepository : RepositoryBase<ToDo>,IToDoRepository
    {
        public ToDoRepository(IDbFactory dbFactory) : base(dbFactory)  
        {
                
        }
    }
}
