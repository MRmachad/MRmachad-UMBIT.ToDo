using AutoMapper;
using UMBIT.ToDo.Dominio.Entidades;

namespace TSE.Nexus.Auth.API.Mapeadores
{
    public class ToDoMapper : Profile
    {
        public ToDoMapper()
        {
            CreateMap<ToDoItem, ToDoItem>();
            CreateMap<ToDoList, ToDoList>();
        }
    }
}
