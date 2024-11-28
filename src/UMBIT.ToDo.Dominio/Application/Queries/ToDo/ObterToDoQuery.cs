using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;
using UMBIT.ToDo.Dominio.Entidades.ToDo;

namespace UMBIT.Nexus.Auth.Dominio.Application.Queries.ToDo
{
    public class ObterToDoQuery : UMBITQuery<ObterToDoQuery, IQueryable<ToDoItem>>
    {
        protected override void Validadors(ValidatorQuery<ObterToDoQuery> validator)
        {
        }
    }
}
