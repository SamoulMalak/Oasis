using Oasis.BL.DTOs.HttpClientCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.BL.IServices
{
    public interface IHttpClientCall
    {
        Task<List<LiveToDoDto>> GetLiveToDoAsync(int pageNumber, int pageSize);
    }
}
