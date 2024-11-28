using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UMBIT.ToDo.Dominio.Entidades.Auth.Basicos;

namespace TSE.Nexus.Auth.Infraestrutura.Contexto
{
    public class IdentityContext : IdentityDbContext<Usuario, Role, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> option) : base(option)
        {

        }
    }
}
