using BarclaysToDos.Services.ToDoItemServices;
using BarclaysToDos.Services.ToDoItemServices.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BarclaysToDos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : Controller
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoController(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoItemDto>>> Index()
        {
            var todoItems = await _toDoItemRepository.GetTodoListItems();
            return todoItems;
        }

        [HttpPost]
        public async Task<ActionResult> AddTodo(ToDoItemDto todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            var newItem = await _toDoItemRepository.AddAsync(todoItem);
            return CreatedAtRoute("Index", new { id = todoItem.Id }, todoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _toDoItemRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoItemDto>> PutTodo(int id, ToDoItemDto todo)
        {
            if (todo == null)
                return BadRequest();

            if (id != todo.Id)
                return BadRequest();

            await _toDoItemRepository.UpdateAsync(todo);
            return CreatedAtRoute("GetTodo", new { id = todo.Id }, todo);
        }
    }
}
