using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;
using UMBIT.ToDo.Dominio.Entidades.Auth.Configuracao;

namespace UMBIT.ToDo.Dominio.Application.Queries.Configuracao
{
    public class ObterStatusDeConfiguracaoQuery : UMBITQuery<ObterStatusDeConfiguracaoQuery, StatusDeConfiguracao>
    {
        protected override void Validadors(ValidatorQuery<ObterStatusDeConfiguracaoQuery> validator)
        {
        }
    }
}
