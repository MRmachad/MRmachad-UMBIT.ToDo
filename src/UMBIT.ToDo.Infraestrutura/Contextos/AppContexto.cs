using Microsoft.EntityFrameworkCore;
using UMBIT.MicroService.SDK.Repositorio.EF;

namespace UMBIT.ToDo.Infraestrutura.Contextos
{
    public class AppContexto : BaseContext<AppContexto>
    {
        public AppContexto(DbContextOptions<AppContexto> options) : base(options)
        {
        }
    }
}
