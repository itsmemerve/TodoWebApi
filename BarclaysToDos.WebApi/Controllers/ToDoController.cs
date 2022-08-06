using BarclaysToDos.Services.ToDoItemServices;
using BarclaysToDos.Services.ToDoItemServices.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BarclaysToDos.WebApi.Controllers
{
    public class ToDoController : BaseApiController
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoController(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoList()
        {
            var todoItems = await _toDoItemRepository.GetTodoListItems();
            return HandlePagedResult(todoItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(ToDoItemDto todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            return HandleResult(await _toDoItemRepository.AddAsync(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            return HandleResult(await _toDoItemRepository.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, ToDoItemDto todo)
        {
            if (todo == null)
                return BadRequest();

            if (id != todo.Id)
                return BadRequest();

            await _toDoItemRepository.UpdateAsync(todo);
            return HandleResult(await _toDoItemRepository.UpdateAsync(todo));
        }
    }
}
