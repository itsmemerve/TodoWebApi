using AutoMapper;
using BarclaysToDos.Data;
using BarclaysToDos.Domain;
using BarclaysToDos.Domain.Constant;
using BarclaysToDos.Services.Core;
using BarclaysToDos.Services.ToDoItemServices;
using BarclaysToDos.Services.ToDoItemServices.Dto;
using BarclaysToDos.Services.ToDoItemServices.Mapper;
using BarclaysToDos.Services.ToDoItemServices.Validation;
using BarclaysToDos.WebApi.Controllers;
using Moq;

namespace BarclaysToDos.Test.Controllers
{
    public class TodoControllerTest
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private NameValidator _validator { get; }
        private readonly IMapper _mapper;
        public TodoControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new ToDoItemProfile()); });
            _mapper = mockMapper.CreateMapper();

            ToDoContext context = new ToDoContext();
            context.TodoItems = GetTestTodos().ToDictionary(x => x.Id, x => x);

            _toDoItemRepository = new ToDoItemRepository(context, _mapper);

            _validator = new NameValidator(_toDoItemRepository);
        }

        [Fact]
        public async Task Index_Returns_ListOf_Todo()
        {
            var result = await _toDoItemRepository.GetTodoListItems();
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Test One", result.Value[0].Name);
        }

        [Fact]
        public async Task Create_ReturnsIsSuccessFalse_GivenNullModel()
        {
            var result = await _toDoItemRepository.AddAsync(null);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Create_ReturnsCreatedTodo_GivenCorrectInputs()
        {
            const string testName = "Test Name";
            
            var newTodo = new ToDoItemDto
            {
                Name = testName,
                Priority = (int)Priority.Low,
                Status = Status.InProgress
            };

            var returnTodo = await _toDoItemRepository.AddAsync(newTodo);

            Assert.Equal(testName, returnTodo.Value.Name);
        }

        [Fact]
        public void Validator_NotAllowSameNamedTask()
        {
            var newTodo = new ToDoItemDto
            {
                Name = "Test One",
                Priority = (int)Priority.Low,
                Status = Status.InProgress
            };

            Assert.False(_validator.Validate(newTodo).IsValid);
        }

        [Fact]
        public async Task Update_ReturnsCreatedTodo_GivenCorrectInputs()
        {
            // Arrange
            const string testName = "Test Name";
            var newTodo = new ToDoItemDto
            {
                Id = 1,
                Name = testName,
                Priority = (int)Priority.Low,
                Status = Status.InProgress
            };

            var returnTodo = await _toDoItemRepository.UpdateAsync(newTodo);
            Assert.True(returnTodo.IsSuccess);
        }

        [Fact]
        public async Task Update_ReturnsIsSuccessFalse_GivenNullModel()
        {
            var returnTodo = await _toDoItemRepository.UpdateAsync(null);
            Assert.False(returnTodo.IsSuccess);
        }

        private static List<ToDoItem> GetTestTodos()
        {
            return new List<ToDoItem>
            {
                new ToDoItem
                {
                    Id = 1,
                    Name = "Test One",
                    Priority = (int)Priority.Low,
                    Status = Status.InProgress
                },
                new ToDoItem
                {
                    Id = 2,
                    Name = "Test Two",
                    Priority = (int)Priority.High,
                    Status = Status.Completed
                }
            };
        }
    }
}
