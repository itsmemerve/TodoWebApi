using AutoMapper;
using BarclaysToDos.Data;
using BarclaysToDos.Domain;
using BarclaysToDos.Services.Core;
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

        public async Task<Result<ToDoItemDto>> AddAsync(ToDoItemDto todoItem)
        {
            if (todoItem == null)
            {
                return Result<ToDoItemDto>.Failure("Item can not be null");
            }

            todoItem.Id = _context.TodoItems.Keys.Max() + 1;
            var item = _mapper.Map<ToDoItem>(todoItem);
            item.CreationTime = DateTime.Now;
            var result = _context.TodoItems.TryAdd(item.Id, item);
            if (result)
                return Result<ToDoItemDto>.Success(todoItem);
            else
                return Result<ToDoItemDto>.Failure("Error has occured.");
        }

        public async Task<Result<string>> DeleteAsync(int todoItemId)
        {
            var item = await FindToDoItemAsync(todoItemId);
            if (item != null)
            {
                if (item.Status == Domain.Constant.Status.Completed)
                {
                    var res = _context.TodoItems.Remove(item.Id);
                    if (res)
                        return Result<string>.Success("Successfully Deleted");
                    else
                        return Result<string>.Failure("Error has occured");
                }
                else
                    return Result<string>.Failure("Only completed items can be deleted.");
            }
            else
                return Result<string>.Success("Item not found");
        }

        /// <summary>
        /// todo: get page number and page size from client 
        /// </summary>
        /// <returns></returns>
        public async Task<Result<PagedList<ToDoItemDto>>> GetTodoListItems()
        {
            var list = GetTodoList();
            var result = await PagedList<ToDoItemDto>.CreateAsync(list.AsQueryable(), 1, 500);
            return Result<PagedList<ToDoItemDto>>.Success(result);
        }

        public async Task<Result<bool>> UpdateAsync(ToDoItemDto todoItem)
        {
            if (todoItem == null)
            {
                return Result<bool>.Failure("Item can not be null");
            }

            var item = await FindToDoItemAsync(todoItem.Id);

            if (item != null)
            {
                if (item.Status != Domain.Constant.Status.Completed && todoItem.Status == Domain.Constant.Status.Completed)
                {
                    item.CompletionTime = DateTime.Now;
                }

                _context.TodoItems[item.Id] = _mapper.Map<ToDoItem>(todoItem);
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure("Item Not Found");
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

        public bool IsExistName(ToDoItemDto dto)
        {
            ToDoItem? toDoItem;
            _context.TodoItems.TryGetValue(dto.Id, out toDoItem);

            if (toDoItem == null)
            {
                return true;
            }

            return toDoItem.Id == dto.Id;
        }

        public List<ToDoItemDto> GetTodoList()
        {
            var list = _context.TodoItems.Select(x => x.Value).ToList();
            var mappedList = _mapper.Map<List<ToDoItemDto>>(list);
            return mappedList;
        }
    }
}
