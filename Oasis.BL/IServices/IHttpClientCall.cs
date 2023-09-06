using Oasis.BL.DTOs.HttpClientCore;

namespace Oasis.BL.IServices
{
    public interface IHttpClientCall
    {
        Task<List<LiveToDoDto>> GetLiveToDoAsync(int pageNumber, int pageSize);
    }
}
