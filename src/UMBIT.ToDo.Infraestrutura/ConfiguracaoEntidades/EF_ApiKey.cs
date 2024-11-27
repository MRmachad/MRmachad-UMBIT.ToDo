using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UMBIT.ToDo.Core.Repositorio.EF;
using UMBIT.ToDo.Dominio.Entidades.Token;

namespace TSE.Nexus.Auth.Infraestrutura.ConfiguracaoDeEntidades
{
    public class EF_ApiKey : CoreEntityConfigurate<ApiToken>
    {
        public override void ConfigureEntidade(EntityTypeBuilder<ApiToken> builder)
        {
        }
    }
}
