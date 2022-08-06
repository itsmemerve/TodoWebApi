using BarclaysToDos.Domain.Constant;
using BarclaysToDos.Services.ToDoItemServices.Validation;
using System.ComponentModel.DataAnnotations;

namespace BarclaysToDos.Services.ToDoItemServices.Dto
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public int Priority { get; set; }
     
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? CompletionTime { get; set; }
    }
}
