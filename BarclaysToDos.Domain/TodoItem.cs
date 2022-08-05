using BarclaysToDos.Domain.Constant;

namespace BarclaysToDos.Domain
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? CompletionTime { get; set; }
    }
}