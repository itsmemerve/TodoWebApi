using BarclaysToDos.Services.ToDoItemServices.Dto;
using FluentValidation;

namespace BarclaysToDos.Services.ToDoItemServices.Validation
{
    public class NameValidator : AbstractValidator<ToDoItemDto>
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        public NameValidator(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;

            RuleFor(x => x.Name)
               .NotEmpty()
               .MinimumLength(2)
               .Must(UniqueNameRule).WithMessage("Todo Item Name field must be unique");

            RuleFor(x => x.Priority).NotNull();
            RuleFor(x => x.Status).NotNull();
        }

        private bool UniqueNameRule(string name)
        {
            var exist = _toDoItemRepository.IsExistName(name);
            if (exist)
                return false;
            return true;
        }
    }
}
