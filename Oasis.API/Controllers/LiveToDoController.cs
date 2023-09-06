using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oasis.BL.DTOs.HttpClientCore;
using Oasis.BL.IServices;

namespace Oasis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveToDoController : ControllerBase
    {
        private readonly IHttpClientCall _httpClientCall;
        public LiveToDoController(IHttpClientCall httpClientCall)
        {
            _httpClientCall = httpClientCall;
        }

        [HttpGet]
        public async Task<List<LiveToDoDto>> GetLiveToDo(int pageNumber,int pageSize) 
        {
            return await _httpClientCall.GetLiveToDoAsync(pageNumber,pageSize);
        }
    }
}
