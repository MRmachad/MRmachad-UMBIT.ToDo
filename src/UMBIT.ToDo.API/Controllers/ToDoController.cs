using Microsoft.AspNetCore.Mvc;
using UMBIT.Nexus.Auth.Contrato;
using UMBIT.Nexus.Auth.Dominio.Application.Queries.ToDo;
using UMBIT.ToDo.API.DTOs;
using UMBIT.ToDo.Core.API.Controllers;
using UMBIT.ToDo.Dominio.Application.Commands.ToDo;
using UMBIT.ToDo.Dominio.Entidades;
using UMBIT.ToDo.Dominio.Interfaces;

namespace UMBIT.ToDo.API.Controllers
{    public class ToDoController : ToDoControllerBase
    {
        public ToDoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public override async Task<ActionResult<ICollection<TarefaDTO>>> ObterTarefa()
        {
            var response = await Mediator.EnviarQuery<ObterToDoQuery, IQueryable<ToDoItem>>(new ObterToDoQuery());
            return UMBITCollectionResponseEntity<TarefaDTO, ToDoItem>(response);
        }
        public override async Task<IActionResult> AdicionarTarefa([FromBody] AdicionarTarefaRequest request)
        {
            var command = Mapper.Map<AdicioneToDoItemCommand>(new AdicioneToDoItemCommand()
            {
                Nome = request.Nome,
                Index = request.Index,
                Status = request.Status,
                DataFim = request.DataFim,
                Descricao = request.Descricao,
                IdToDoList = request.IdToDoList,
                DataInicio = request.DataInicio,
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        public override async Task<ActionResult<TarefaDTO>> ObterUnicaTarefa(Guid id)
        {
            var response = await Mediator.EnviarQuery<ObterToDoPorIdQuery, ToDoItem>(new ObterToDoPorIdQuery(id));
            return UMBITResponse<TarefaDTO, ToDoItem>(response);
        }

        public override async Task<IActionResult> AtualizarTarefa(Guid id, [FromBody] AtualizarTarefaRequest request)
        {
            var command = Mapper.Map<EditeToDoItemCommand>(new EditeToDoItemCommand()
            {
                Id = request.Id,
                Nome = request.Nome,
                Status = request.Status,
                DataFim = request.DataFim,
                Descricao = request.Descricao,
                DataInicio = request.DataInicio,
                IdToDoList = request.IdToDoList,
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        public override async Task<IActionResult> RemoverTarefa(Guid id)
        {
            var command = Mapper.Map<DeleteToDoItemCommand>(new DeleteToDoItemCommand()
            {
                Id = id
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        public override async Task<ActionResult<ICollection<ListaDTO>>> ObterListaTarefa()
        {
            var response = await Mediator.EnviarQuery<ObterToDoListQuery, IQueryable<ToDoList>>(new ObterToDoListQuery());
            return UMBITCollectionResponseEntity<ListaDTO, ToDoList>(response);
        }

        public override async Task<IActionResult> AdicionarListaTarefa([FromBody] AdicionarListaRequest request)
        {
            var command = Mapper.Map<AdicioneToDoListCommand>(new AdicioneToDoListCommand()
            {
                Nome = request.Nome,
                Items = request.Tarefas?.Select(itemDTO => (itemDTO.Nome, itemDTO.Descricao, itemDTO.DataInicio, itemDTO.DataFim, itemDTO.Status))?.ToList()
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        public override async Task<ActionResult<ListaDTO>> ObterUnicaListaTarefa(Guid id)
        {
            var response = await Mediator.EnviarQuery<ObterToDoListPorIdQuery, ToDoList>(new ObterToDoListPorIdQuery(id));
            return UMBITResponse<ListaDTO, ToDoList>(response);
        }

        public override async Task<IActionResult> AtualizarListaTarefa(Guid id, [FromBody] AtualizarListaRequest request)
        {
            var command = Mapper.Map<EditeToDoListItemCommand>(new EditeToDoListItemCommand()
            {
                Id = request.Id,
                Nome = request.Nome
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        public override async Task<IActionResult> RemoverListaTarefa(Guid id)
        {
            var command = Mapper.Map<DeleteToDoListCommand>(new DeleteToDoListCommand()
            {
                Id = id
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }
    }
}
