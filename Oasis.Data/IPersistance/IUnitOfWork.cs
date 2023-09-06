using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.Data.IPersistance
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
