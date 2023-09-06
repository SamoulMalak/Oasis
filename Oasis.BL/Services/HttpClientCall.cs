using Newtonsoft.Json;
using Oasis.BL.DTOs.HttpClientCore;
using Oasis.BL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.BL.Services
{
    public class HttpClientCall : IHttpClientCall
    {
        public async Task<List<LiveToDoDto>> GetLiveToDoAsync(int pageNumber,int pageSize)
        {
            var todos = new List<LiveToDoDto>();
            
            string apiUrl = "https://jsonplaceholder.typicode.com/todos";
           
            using (HttpClient client = new HttpClient())
            {
               
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    
                    string jsonResult = await response.Content.ReadAsStringAsync();
                     todos = JsonConvert.DeserializeObject<List<LiveToDoDto>>(jsonResult);
                     List<LiveToDoDto> pagedTodos = todos.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                     return pagedTodos;
                }


            }
            return todos;
           
         

           
            
        }
    }
}
