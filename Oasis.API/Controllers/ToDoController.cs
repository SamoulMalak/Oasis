using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oasis.BL.DTOs.ToDoDto;
using Oasis.BL.IServices;

namespace Oasis.API.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoServices toDoServices;

        public ToDoController(IToDoServices toDoServices)
        {
            this.toDoServices = toDoServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoAsync(CreateToDoDto item)
        {
           var result = await toDoServices.CreateToDoAsync(item);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Failed to add the element.");
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetToDoById(int Id)
        {
           ViewToDoDto item= toDoServices.GetToDoItemById(Id);
            if (item!=null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        [HttpPut("{itemId:int}")]
        public IActionResult UpdateToDoItem([FromBody] UpdateToDoDto updateItem)
        {
            bool result = toDoServices.UpdateToDoItem(updateItem);
            if (result) 
            {
                return Ok(updateItem);
            }
            return BadRequest("Can't Edit this item");
        }


        [HttpDelete("{Id:int}")]
        public IActionResult DeleteToDoItem(int Id)
        {
            bool result =toDoServices.DeleteToDo(Id);
            if (result) 
            {
                return Ok();
            }
            return BadRequest("Can't Find and Delete this item");
        }
    }
}
