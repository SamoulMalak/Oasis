using Oasis.Data.Entities;
using Oasis.Data.IPersistance;
using Oasis.Data.IRepositories;
using Oasis.Data.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.Data.Repositories
{
    public class ToDoRepository : RepositoryBase<ToDo>,IToDoRepository
    {
        public ToDoRepository(IDbFactory dbFactory) : base(dbFactory)  
        {
                
        }
    }
}
