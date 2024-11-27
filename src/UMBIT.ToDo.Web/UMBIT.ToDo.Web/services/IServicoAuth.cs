using Refit;
using UMBIT.ToDo.Web.Models;

namespace UMBIT.ToDo.Web.services
{
    public interface IServicoAuth
    {
        [Get("/auth-status")]
        Task<AuthStatusDTO> CheckAuth();

        [Post("/adicionar-administrador")]
        Task<AuthStatusDTO> AdicionarAdministrador([Body] AdicionarAdministradorRequestDTO adicionarAdministradorRequestDTO);

    }
}
