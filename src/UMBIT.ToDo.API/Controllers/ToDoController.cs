using Microsoft.AspNetCore.Mvc;
using UMBIT.Nexus.Auth.Dominio.Application.Queries.ToDo;
using UMBIT.ToDo.API.DTOs;
using UMBIT.ToDo.Core.API.Controllers;
using UMBIT.ToDo.Dominio.Application.Commands.ToDo;
using UMBIT.ToDo.Dominio.Entidades;
using UMBIT.ToDo.Dominio.Interfaces;

namespace UMBIT.ToDo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : BaseController
    {
        public ToDoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpDelete("/delete-item/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            var command = Mapper.Map<DeleteToDoItemCommand>(new DeleteToDoItemCommand()
            {
                Id = id
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }
        [HttpDelete("/delete-list/{id}")]
        public async Task<IActionResult> DeleteList([FromRoute] Guid id)
        {
            var command = Mapper.Map<DeleteToDoListCommand>(new DeleteToDoListCommand()
            {
                Id = id
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        [HttpPost("/adicione-item")]
        public async Task<IActionResult> AdicioneItem([FromBody] ToDoItemDTO itemDTO)
        {
            var command = Mapper.Map<AdicioneToDoItemCommand>(new AdicioneToDoItemCommand()
            {
                Nome = itemDTO.Nome,
                Index = itemDTO.Index,
                Status = itemDTO.Status,
                DataFim = itemDTO.DataFim,
                Descricao = itemDTO.Descricao,
                IdToDoList = itemDTO.IdToDoList,
                DataInicio = itemDTO.DataInicio,
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        [HttpPost("/adicione-list")]
        public async Task<IActionResult> AdicioneList([FromBody] ToDoListDTO toDoListDTO)
        {
            var command = Mapper.Map<AdicioneToDoListCommand>(new AdicioneToDoListCommand()
            {
                Nome = toDoListDTO.Nome,
                Items = toDoListDTO.Items?.Select(itemDTO => (itemDTO.Nome, itemDTO.Descricao, itemDTO.DataInicio, itemDTO.DataFim, itemDTO.Status))?.ToList()
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        [HttpPost("/edite-item")]
        public async Task<IActionResult> EditeItem([FromBody] ToDoItemDTO toDoItemDTO)
        {
            var command = Mapper.Map<EditeToDoItemCommand>(new EditeToDoItemCommand()
            {
                Id = toDoItemDTO.Id,
                Nome = toDoItemDTO.Nome,
                Status = toDoItemDTO.Status,
                DataFim = toDoItemDTO.DataFim,
                Descricao = toDoItemDTO.Descricao,
                DataInicio = toDoItemDTO.DataInicio,
                IdToDoList = toDoItemDTO.IdToDoList,
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        [HttpPost("/edite-list")]
        public async Task<IActionResult> EditeLista([FromBody] ToDoListDTO toDoListDTO)
        {
            var command = Mapper.Map<EditeToDoListItemCommand>(new EditeToDoListItemCommand()
            {
                Id = toDoListDTO.Id,
                Nome = toDoListDTO.Nome
            });

            var response = await Mediator.EnviarComando(command);

            return UMBITResponse(response);
        }

        [HttpGet("/obtenha-item/{id}")]
        public async Task<ActionResult<ToDoItem>> ObtenhaItem([FromRoute()] Guid id)
        {
            var response = await Mediator.EnviarQuery<ObterToDoPorIdQuery, ToDoItem>(new ObterToDoPorIdQuery(id));
            return UMBITResponse<ToDoItem, ToDoItem>(response);
        }

        [HttpGet("/obtenha-list/{id}")]
        public async Task<ActionResult<ToDoList>> ObtenhaLista([FromRoute()] Guid id)
        {
            var response = await Mediator.EnviarQuery<ObterToDoListPorIdQuery, ToDoList>(new ObterToDoListPorIdQuery(id));
            return UMBITResponse<ToDoList, ToDoList>(response);

        }
        [HttpGet("/obtenha-item")]
        public async Task<ActionResult<ICollection<ToDoItem>>> ObtenhaItem()
        {
            var response = await Mediator.EnviarQuery<ObterToDoQuery, IQueryable<ToDoItem>>(new ObterToDoQuery());
            return UMBITCollectionResponseEntity<ToDoItem, ToDoItem>(response);
        }
        [HttpGet("/obtenha-list")]
        public async Task<ActionResult<ICollection<ToDoList>>> ObtenhaLista()
        {
            var response = await Mediator.EnviarQuery<ObterToDoListQuery, IQueryable<ToDoList>>(new ObterToDoListQuery());
            return UMBITCollectionResponseEntity<ToDoList, ToDoList>(response);
        }
    }
}
