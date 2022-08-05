using BarclaysToDos.Domain;

namespace BarclaysToDos.Data
{
    public class ToDoContext
    {
        public Dictionary<int, ToDoItem> TodoItems { get; set; }

    }

}
