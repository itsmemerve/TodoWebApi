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

        /// <summary>
        /// This method returns all to-do lists.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetTodoList()
        {
            var todoItems = await _toDoItemRepository.GetTodoListItems();
            return HandlePagedResult(todoItems);
        }

        /// <summary>
        /// This method helps to add new todo item.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddTodo(ToDoItemDto todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            return HandleResult(await _toDoItemRepository.AddAsync(todoItem));
        }

        /// <summary>
        /// This method helps to delete todo item by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            return HandleResult(await _toDoItemRepository.DeleteAsync(id));
        }

        /// <summary>
        /// This method updates todo item. Name, Status and Priority fields are required
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, ToDoItemDto todo)
        {
            if (todo == null)
                return BadRequest();

            if (id != todo.Id)
                return BadRequest();

            return HandleResult(await _toDoItemRepository.UpdateAsync(todo));
        }
    }
}
