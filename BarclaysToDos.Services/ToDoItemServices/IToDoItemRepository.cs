using BarclaysToDos.Services.Core;
using BarclaysToDos.Services.ToDoItemServices.Dto;

namespace BarclaysToDos.Services.ToDoItemServices
{
    public interface IToDoItemRepository
    {
        Task<Result<PagedList<ToDoItemDto>>> GetTodoListItems();
        List<ToDoItemDto> GetTodoList();
        Task<ToDoItemDto> FindToDoItemAsync(int id);
        Task<Result<ToDoItemDto>> AddAsync(ToDoItemDto todoItem);
        Task<Result<bool>> UpdateAsync(ToDoItemDto todoItem);
        Task<Result<string>> DeleteAsync(int todoItemId);
        bool IsExistName(ToDoItemDto dto);
    }
}
