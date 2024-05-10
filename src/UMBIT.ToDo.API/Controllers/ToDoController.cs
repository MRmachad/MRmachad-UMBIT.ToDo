using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UMBIT.ToDo.SDK.API.Controllers;
using UMBIT.ToDo.API.DTOs;
using UMBIT.ToDo.Dominio.Basicos.Enum;
using UMBIT.ToDo.Dominio.Entidades;
using UMBIT.ToDo.Dominio.Interfaces;

namespace UMBIT.ToDo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : BaseController
    {
        private readonly IServicoDeToDo ServicoDeTodo;
        public ToDoController(IServiceProvider serviceProvider, IServicoDeToDo servicoDeToDo) : base(serviceProvider)
        {
            this.ServicoDeTodo = servicoDeToDo;
        }

        [HttpDelete("/delete-item/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await this.ServicoDeTodo.DeleteToDoItem(id);
            });
        }
        [HttpDelete("/delete-list/{id}")]
        public async Task<IActionResult> DeleteList([FromRoute] Guid id)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await this.ServicoDeTodo.DeleteToDoList(id);
            });
        }
        [HttpPost("/adicione-item")]
        public async Task<IActionResult> AdicioneItem([FromBody] ToDoItemDTO itemDTO)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await this.ServicoDeTodo.AdicioneToDoItem(new ToDoItem()
                {
                    Nome = itemDTO.Nome,
                    Index = itemDTO.Index,
                    Descricao = itemDTO.Descricao,
                    IdToDoList = itemDTO.IdToDoList,
                    DataFim = itemDTO.DataFim,
                    DataInicio = itemDTO.DataInicio,
                    Status = Dominio.Basicos.Enum.EnumeradorStatus.Parse<EnumeradorStatus>(itemDTO.Status.ToString()),

                });
            });
        }

        [HttpPost("/adicione-list")]
        public async Task<IActionResult> AdicioneList([FromBody] ToDoListDTO toDoListDTO)
        {
            return await MiddlewareDeRetorno(async () =>
            {
#pragma warning disable CS8604 // Possible null reference argument.
                await this.ServicoDeTodo.AdicioneToDoList(toDoListDTO.Nome, toDoListDTO.Items?.Select(itemDTO =>
                {
                    return new ToDoItem()
                    {
                        Index = itemDTO.Index,
                        Descricao = itemDTO.Descricao,
                        IdToDoList = itemDTO.IdToDoList,
                        Status = Dominio.Basicos.Enum.EnumeradorStatus.Parse<EnumeradorStatus>(itemDTO.Status.ToString()),

                    };
                })?.ToList());
#pragma warning restore CS8604 // Possible null reference argument.
            });
        }
        [HttpPost("/edite-item")]
        public async Task<IActionResult> EditeItem([FromBody] ToDoItemDTO toDoItemDTO)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await this.ServicoDeTodo.EditeToDoItem(toDoItemDTO.Id, toDoItemDTO.IdToDoList, toDoItemDTO.DataFim, toDoItemDTO.DataInicio, toDoItemDTO.Nome, toDoItemDTO.Descricao, toDoItemDTO.Status);
            });
        }

        [HttpPost("/edite-list")]
        public async Task<IActionResult> EditeLista([FromBody] ToDoListDTO toDoListDTO)
        {
            return await MiddlewareDeRetorno(async () =>
            {
               await this.ServicoDeTodo.EditeToDoListItem(toDoListDTO.Id, toDoListDTO.Nome);
            });
        }

        [HttpGet("/obtenha-item/{id}")]
        public async Task<ActionResult<ToDoItem>> ObtenhaItem([FromRoute()] Guid id)
        {
            return await MiddlewareDeRetorno<ToDoItem>(async () =>
            {
                return await this.ServicoDeTodo.ObtenhaToDoItem(id);
            });
        }
        [HttpGet("/obtenha-list/{id}")]
        public async Task<ActionResult<ToDoList>> ObtenhaLista([FromRoute()] Guid id)
        {
            return await MiddlewareDeRetorno<ToDoList>(async () =>
            {
                return await this.ServicoDeTodo.ObtenhaToDoList(id);
            });
        }
        [HttpGet("/obtenha-item")]
        public async Task<ActionResult<List<ToDoItem>>> ObtenhaItem()
        {
            return await MiddlewareDeRetorno<List<ToDoItem>>(async () =>
            {
                return await this.ServicoDeTodo.ObtenhaToDoItems();
            });
        }
        [HttpGet("/obtenha-list")]
        public async Task<ActionResult<List<ToDoList>>> ObtenhaLista()
        {
            return await MiddlewareDeRetorno<List<ToDoList>>(async () =>
            {
                return await this.ServicoDeTodo.ObtenhaToDoLists();
            });
        }
    }
}
