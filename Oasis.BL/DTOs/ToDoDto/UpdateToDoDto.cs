

namespace Oasis.BL.DTOs.ToDoDto
{
    public class UpdateToDoDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Completed { get; set; }
    }
}
