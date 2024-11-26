using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;
using UMBIT.ToDo.Dominio.Entidades;

namespace UMBIT.Nexus.Auth.Dominio.Application.Queries.ToDo
{
    public class ObterToDoListQuery : UMBITQuery<ObterToDoListQuery, IQueryable<ToDoList>>
    {
        protected override void Validadors(ValidatorQuery<ObterToDoListQuery> validator)
        {
        }
    }
}
