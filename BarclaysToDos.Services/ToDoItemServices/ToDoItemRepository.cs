using AutoMapper;
using BarclaysToDos.Data;
using BarclaysToDos.Domain;
using BarclaysToDos.Services.ToDoItemServices.Dto;

namespace BarclaysToDos.Services.ToDoItemServices
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ToDoContext _context;
        public readonly IMapper _mapper;

        public ToDoItemRepository(ToDoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ToDoItemDto> AddAsync(ToDoItemDto todoItem)
        {
            todoItem.Id = _context.TodoItems.Keys.Max() + 1;
            var item = _mapper.Map<ToDoItem>(todoItem);
            _context.TodoItems.Add(item.Id, item);
            return todoItem;
        }

        public async Task<bool> DeleteAsync(int todoItemId)
        {
            var item = await FindToDoItemAsync(todoItemId);
            if (item != null)
            {
                if (item.Status == Domain.Constant.Status.Completed)
                {
                    _context.TodoItems.Remove(item.Id);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<List<ToDoItemDto>> GetTodoListItems()
        {
            List<ToDoItem> list = _context.TodoItems.Select(x => x.Value).ToList();
            List<ToDoItemDto> result = _mapper.Map<List<ToDoItemDto>>(list);
            return result;
        }

        public async Task<ToDoItemDto> UpdateAsync(ToDoItemDto todoItem)
        {
            var item = await FindToDoItemAsync(todoItem.Id);

            if (item != null)
            {
                _context.TodoItems[item.Id] = _mapper.Map<ToDoItem>(item);
            }

            return todoItem;
        }

        public async Task<ToDoItemDto> FindToDoItemAsync(int id)
        {
            ToDoItem? toDoItem;

            _context.TodoItems.TryGetValue(id, out toDoItem);

            if (toDoItem != null)
            {
                return _mapper.Map<ToDoItemDto>(toDoItem);
            }

            else return null;
        }
    }
}
