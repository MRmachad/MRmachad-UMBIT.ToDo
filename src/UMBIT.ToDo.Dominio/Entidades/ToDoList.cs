using FluentValidation;
using UMBIT.ToDo.Core.Repositorio.Data;

namespace UMBIT.ToDo.Dominio.Entidades
{
    public class ToDoList : BaseEntity<ToDoList>
    {
        public string?  Nome { get;set; }

        public virtual List<ToDoItem> ToDoItems { get; set; }

        protected override void Validadors(Validator<ToDoList> validator)
        {
            validator.RuleFor(x => x.Nome)
                     .Matches(@"^[a-zA-Z\s]+$").WithMessage("O nome deve conter apenas letras.")
                     .MaximumLength(50).WithMessage("O nome deve ter no máximo 50 caracteres.");

        }
    }
}
