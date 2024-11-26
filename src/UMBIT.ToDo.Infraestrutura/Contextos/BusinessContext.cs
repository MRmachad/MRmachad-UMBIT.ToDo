using Microsoft.EntityFrameworkCore;
using UMBIT.ToDo.Core.Repositorio.EF;

namespace UMBIT.ToDo.Infraestrutura.Contextos
{
    public class BusinessContext : BaseContext<BusinessContext>
    {
        public BusinessContext(DbContextOptions<BusinessContext> options) : base(options)
        {
        }
    }
}
