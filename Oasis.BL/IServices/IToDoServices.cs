using Oasis.BL.DTOs.ToDoDto;


namespace Oasis.BL.IServices
{
    public interface IToDoServices
    {
        Task<bool> CreateToDoAsync(CreateToDoDto toDoDto);
        ViewToDoDto GetToDoItemById(int Id);
        bool DeleteToDo(int Id);
        bool UpdateToDoItem(UpdateToDoDto UpdatedItem);

    }
}
