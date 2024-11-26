using FluentValidation;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;
using UMBIT.ToDo.Dominio.Entidades;

namespace UMBIT.Nexus.Auth.Dominio.Application.Queries.ToDo
{
    public class ObterToDoPorIdQuery : UMBITQuery<ObterToDoPorIdQuery, ToDoItem>
    {
        public Guid Id { get; private set; }

        protected ObterToDoPorIdQuery() { }


        public ObterToDoPorIdQuery(Guid id)
        {
            this.Id = id;
        }

        protected override void Validadors(ValidatorQuery<ObterToDoPorIdQuery> validator)
        {
            validator
                .RuleFor(qry => qry.Id)
                .NotEqual(Guid.Empty).WithMessage("Id de usuário inválido");
        }
    }
}
