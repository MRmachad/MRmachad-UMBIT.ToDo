using Refit;
using UMBIT.ToDo.Web.Models;

namespace UMBIT.ToDo.Web.services
{
    public interface IServicoDeToDo
    {
        [Post("/adicione-item")]
        Task AdicioneItem([Body] TaskDTO taskDTO);

        [Post("/edite-item")]
        Task EditItem([Body] TaskDTO taskDTO);

        [Delete("/delete-item/{id}")]
        Task DeleteItem([Refit.AliasAs("id")] Guid id);

        [Get("/obtenha-item/{id}")]
        Task<TaskDTO> ObtenhaItem([Refit.AliasAs("id")] Guid id);

        [Get("/obtenha-item")]
        Task<IEnumerable<TaskDTO>> ObtenhaItens();

        [Post("/adicione-list")]
        Task AdicioneList([Body] ListTaskDTO taskDTO);

        [Post("/edite-list")]
        Task EditList([Body] ListTaskDTO taskDTO);

        [Delete("/delete-list/{id}")]
        Task DeleteList([Refit.AliasAs("id")] Guid id);

        [Get("/obtenha-list/{id}")]
        Task<ListTaskDTO> ObtenhaList([Refit.AliasAs("id")] Guid id);

        [Get("/obtenha-list")]
        Task<IEnumerable<ListTaskDTO>> ObtenhaLists();
    }
}
