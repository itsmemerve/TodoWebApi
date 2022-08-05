using BarclaysToDos.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BarclaysToDos.Data
{
    public class AppDbInitializer
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ToDoContext>();

                if (context.TodoItems != null && context.TodoItems.Any())
                    return;

                context.TodoItems = GetTodos().ToDictionary(x => x.Id, x => x);
            }
        }

        public static List<ToDoItem> GetTodos()
        {
            List<ToDoItem> todos = new List<ToDoItem>() {
                new ToDoItem {Id = 1,Name="One", Status= Domain.Constant.Status.NotStarted, CreationTime=DateTime.Now, CompletionTime=null, Priority = (int)Domain.Constant.Priority.Low},
                new ToDoItem {Id = 2,Name="Two", Status= Domain.Constant.Status.NotStarted, CreationTime=DateTime.Now, CompletionTime=null, Priority = (int)Domain.Constant.Priority.Low},
                new ToDoItem {Id = 3, Name="Three", Status= Domain.Constant.Status.InProgress, CreationTime=DateTime.Now, CompletionTime=null, Priority = (int)Domain.Constant.Priority.High},
                new ToDoItem {Id = 4, Name="Four", Status= Domain.Constant.Status.Completed, CreationTime=DateTime.Now, CompletionTime=DateTime.Now, Priority = (int)Domain.Constant.Priority.Medium},
                new ToDoItem {Id = 5, Name="Five", Status= Domain.Constant.Status.InProgress, CreationTime=DateTime.Now, CompletionTime=null, Priority = (int)Domain.Constant.Priority.Medium},
                new ToDoItem {Id = 6, Name="Six", Status= Domain.Constant.Status.Completed, CreationTime=DateTime.Now, CompletionTime=DateTime.Now, Priority = (int)Domain.Constant.Priority.Low}
            };
            return todos;
        }
    }
}
