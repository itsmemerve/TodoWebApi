using BarclaysToDos.Domain.Constant;
using BarclaysToDos.Services.ToDoItemServices;
using BarclaysToDos.Services.ToDoItemServices.Dto;
using BarclaysToDos.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BarclaysToDos.Test.Controllers
{
    public class TodoControllerTest
    {
        [Fact]
        public async Task Index_Returns_ListOf_Todo()
        {
            // Arrange
            var mockRepo = new Mock<IToDoItemRepository>();

            mockRepo.Setup(repo => repo.GetTodoListItems())
                .Returns(GetTestTodos());

            var controller = new ToDoController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.Equal(2, result.Value.Count());
            Assert.Equal("Test One", result.Value.FirstOrDefault().Name);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_GivenNullModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IToDoItemRepository>();
            var controller = new ToDoController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.AddTodo(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedTodo_GivenCorrectInputs()
        {
            // Arrange
            const string testName = "Test Name";
            var newTodo = new ToDoItemDto
            {
                Name = testName,
                Priority = (int)Priority.Low,
                Status = Status.InProgress
            };
            var mockRepo = new Mock<IToDoItemRepository>();
            var controller = new ToDoController(mockRepo.Object);

            // Act
            var result = await controller.AddTodo(newTodo);

            // Assert
            var okResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnTodo = Assert.IsType<ToDoItemDto>(okResult.Value);
            Assert.Equal(testName, returnTodo.Name);
        }

        [Fact]
        public async Task Update_ReturnsCreatedTodo_GivenCorrectInputs()
        {
            // Arrange
            const string testName = "Test Name";
            var newTodo = new ToDoItemDto
            {
                Name = testName,
                Priority = (int)Priority.Low,
                Status = Status.InProgress
            };
            var mockRepo = new Mock<IToDoItemRepository>();
            var controller = new ToDoController(mockRepo.Object);

            // Act
            var result = await controller.AddTodo(newTodo);

            // Assert
            var okResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnTodo = Assert.IsType<ToDoItemDto>(okResult.Value);
            Assert.Equal(testName, returnTodo.Name);
        }

        private static async Task<List<ToDoItemDto>> GetTestTodos()
        {
            return new List<ToDoItemDto>
            {
                new ToDoItemDto
                {
                    Id = 1,
                    Name = "Test One",
                    Priority = (int)Priority.Low,
                    Status = Status.InProgress
                },
                new ToDoItemDto
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
