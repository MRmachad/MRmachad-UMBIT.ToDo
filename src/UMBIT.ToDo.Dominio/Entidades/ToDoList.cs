using UMBIT.ToDo.SDK.Entidades;

namespace UMBIT.ToDo.Dominio.Entidades
{
    public class ToDoList : BaseEntity
    {
        public string?  Nome { get;set; }

        public virtual List<ToDoItem> ToDoItems { get; set; }
    }
}
