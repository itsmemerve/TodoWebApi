using BarclaysToDos.Services.ToDoItemServices.Dto;

namespace BarclaysToDos.Services.ToDoItemServices
{
    public interface IToDoItemRepository
    {
        Task<List<ToDoItemDto>> GetTodoListItems();
        Task<ToDoItemDto> FindToDoItemAsync(int id);
        Task<ToDoItemDto> AddAsync(ToDoItemDto todoItem);
        Task<ToDoItemDto> UpdateAsync(ToDoItemDto todoItem);
        Task<bool> DeleteAsync(int todoItemId);
    }
}
