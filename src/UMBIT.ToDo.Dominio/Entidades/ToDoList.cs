using UMBIT.ToDo.Core.Repositorio.Data;

namespace UMBIT.ToDo.Dominio.Entidades
{
    public class ToDoList : BaseEntity<ToDoList>
    {
        public string?  Nome { get;set; }

        public virtual List<ToDoItem> ToDoItems { get; set; }

        protected override void Validadors(Validator<ToDoList> validator)
        {
        }
    }
}
