using Oasis.BL.DTOs.ToDoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.BL.IServices
{
    public interface IToDoServices
    {
        Task<bool> CreateToDoAsync(Create_Update_ToDoDto toDoDto);
        ViewToDoDto GetToDoItemById(int Id);
        bool DeleteToDo(int Id);

        bool UpdateToDoItem(int Id, Create_Update_ToDoDto NewItem);

    }
}
