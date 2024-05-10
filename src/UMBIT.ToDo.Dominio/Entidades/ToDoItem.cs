using UMBIT.MicroService.SDK.Entidades;
using UMBIT.ToDo.Dominio.Basicos.Enum;

namespace UMBIT.ToDo.Dominio.Entidades
{
    public class ToDoItem: BaseEntity
    {
        public Guid? IdToDoList { get; set; }
        public int Index { get; set; }
        public string Nome { get;set; }
        public string? Descricao { get;set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public EnumeradorStatus Status { get;set; }
        public virtual ToDoList ToDoList { get; set; }
    }
}
