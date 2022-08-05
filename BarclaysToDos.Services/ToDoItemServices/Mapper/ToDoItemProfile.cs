using AutoMapper;
using BarclaysToDos.Domain;
using BarclaysToDos.Services.ToDoItemServices.Dto;

namespace BarclaysToDos.Services.ToDoItemServices.Mapper
{
    public class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<ToDoItem, ToDoItemDto>();
            CreateMap<ToDoItemDto, ToDoItem>();
        }
    }
}
