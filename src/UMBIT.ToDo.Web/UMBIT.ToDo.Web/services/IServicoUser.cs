using Refit;
using UMBIT.Nexus.Auth.Contrato;

namespace UMBIT.ToDo.Web.services
{
    public interface IServicoUser
    {
        [Get("/usuarios")]
        Task<List<UsuarioResponseDTO>> GetUsuarios();

        [Delete("/usuario/{id}")]
        Task RemoverUsuario([AliasAs("id")] Guid id);

        [Delete("/avise-tarefa/{id}")]
        Task AviseUsuario([AliasAs("id")] Guid id);


    }
}
