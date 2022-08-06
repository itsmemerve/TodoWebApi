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
               .MinimumLength(2);
            
            RuleFor(x => x).Must(UniqueNameRule);

            RuleFor(x => x.Priority).NotNull();
            RuleFor(x => x.Status).NotNull();
        }

        private bool UniqueNameRule(ToDoItemDto dto)
        {
            return _toDoItemRepository.IsExistName(dto);
        }
    }
}
