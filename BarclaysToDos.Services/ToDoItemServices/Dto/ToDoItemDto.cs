using BarclaysToDos.Domain.Constant;
using BarclaysToDos.Services.ToDoItemServices.Validation;
using System.ComponentModel.DataAnnotations;

namespace BarclaysToDos.Services.ToDoItemServices.Dto
{
    public class ToDoItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Priority { get; set; }
     
        [Required, NameCanNotBeSame]
        public string Name { get; set; }
        [Required]
        public Status Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? CompletionTime { get; set; }
    }
}
