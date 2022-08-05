using BarclaysToDos.Data;
using System.ComponentModel.DataAnnotations;

namespace BarclaysToDos.Services.ToDoItemServices.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NameCanNotBeSameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (ToDoContext?)validationContext.GetService(typeof(ToDoContext));

            if (_context == null)
                return new ValidationResult("Context Error! Please contact with development team!");

            if (value == null)
                return ValidationResult.Success;

            var name = value as string;
            if (String.IsNullOrEmpty(name))
                return new ValidationResult("Name can not be null");

            var isExist = _context.TodoItems.Any(x => x.Value.Name == name);

            if (isExist)
                return new ValidationResult("Name can not be same");
            
            return ValidationResult.Success;
        }
    }
}
