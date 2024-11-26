using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UMBIT.ToDo.Core.Messages.Messagem;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;
using UMBIT.ToDo.Core.Messages.Messagem.Interfaces;
using UMBIT.ToDo.Core.Notificacao.Interfaces;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;
using UMBIT.ToDo.Core.Seguranca.Models;
using UMBIT.ToDo.Dominio.Application.Commands.ToDo;
using UMBIT.ToDo.Dominio.Basicos.Enum;
using UMBIT.ToDo.Dominio.Configuradores;
using UMBIT.ToDo.Dominio.Entidades;
using UMBIT.ToDo.Dominio.Entidades.Basicos;

namespace UMBIT.ToDo.Dominio.Application.Commands
{
    public class ToDoCommandHandler :
        UMBITCommandHandlerBase,
        IUMBITCommandRequestHandler<AdicioneToDoItemCommand>,
        IUMBITCommandRequestHandler<AdicioneToDoListCommand>,
        IUMBITCommandRequestHandler<DeleteToDoItemCommand>,
        IUMBITCommandRequestHandler<DeleteToDoListCommand>,
        IUMBITCommandRequestHandler<EditeToDoItemCommand>,
        IUMBITCommandRequestHandler<EditeToDoListItemCommand>,
        IUMBITCommandRequestHandler<FinalizeToDoItemCommand>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ContextoPrincipal _contextoPrincipal;
        private readonly IOptions<IdentitySettings> _identitySettings;

        private readonly IRepositorio<ToDoItem> RepositorioToDoItem;
        private readonly IRepositorio<ToDoList> RepositorioToDoList;

        public ToDoCommandHandler(
            IUnidadeDeTrabalhoNaoTransacional unidadeDeTrabalho,
            INotificador notificador,
            ContextoPrincipal contextoPrincipal,
            UserManager<Usuario> userManager,
            IOptions<IdentitySettings> identitySettings) : base(unidadeDeTrabalho, notificador)
        {
            _userManager = userManager;
            _identitySettings = identitySettings;
            _contextoPrincipal = contextoPrincipal;
            RepositorioToDoList = UnidadeDeTrabalho.ObterRepositorio<ToDoList>();
            RepositorioToDoItem = UnidadeDeTrabalho.ObterRepositorio<ToDoItem>();

        }

        async Task<UMBITMessageResponse> IRequestHandler<AdicioneToDoItemCommand, UMBITMessageResponse>.Handle(AdicioneToDoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var toDoItem = new ToDoItem()
                {
                    Nome = request.Nome,
                    Index = request.Index,
                    Descricao = request.Descricao,
                    IdToDoList = request.IdToDoList,
                    DataFim = request.DataFim,
                    DataInicio = request.DataInicio,
                    Status = Enum.Parse<EnumeradorStatus>(request.Status.ToString()),

                };

                await RepositorioToDoItem.Adicionar(toDoItem);
                await UnidadeDeTrabalho.SalveAlteracoes();

                return CommandResponse();

            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task<UMBITMessageResponse> IRequestHandler<AdicioneToDoListCommand, UMBITMessageResponse>.Handle(AdicioneToDoListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var toDolist = new ToDoList();

                toDolist.Id = Guid.NewGuid();
                toDolist.Nome = request.Nome;

                await RepositorioToDoList.Adicionar(toDolist);

                if (request.Items != null && request.Items.Any())
                    foreach (var toDoItemList in request.Items)
                    {
                        var toDoItem = new ToDoItem()
                        {

                            Nome = toDoItemList.Nome,
                            IdToDoList = toDolist.Id,
                            DataFim = toDoItemList.DataFim,
                            Descricao = toDoItemList.Descricao,
                            DataInicio = toDoItemList.DataInicio,
                            Index = request.Items.IndexOf(toDoItemList),
                            Status = Enum.Parse<EnumeradorStatus>(toDoItemList.Status.ToString()),

                        };

                        await RepositorioToDoItem.Adicionar(toDoItem);
                    }

                await UnidadeDeTrabalho.SalveAlteracoes();
                return CommandResponse();
            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task<UMBITMessageResponse> IRequestHandler<DeleteToDoItemCommand, UMBITMessageResponse>.Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await RepositorioToDoItem.Query().SingleAsync(t => t.Id == request.Id);
                RepositorioToDoItem.Remover(item);

                await UnidadeDeTrabalho.SalveAlteracoes();
                return CommandResponse();

            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<UMBITMessageResponse> IRequestHandler<DeleteToDoListCommand, UMBITMessageResponse>.Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var list = await RepositorioToDoList.Query().SingleAsync(t => t.Id == request.Id);
                RepositorioToDoList.Remover(list);

                await UnidadeDeTrabalho.SalveAlteracoes();
                return CommandResponse();
            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task<UMBITMessageResponse> IRequestHandler<EditeToDoItemCommand, UMBITMessageResponse>.Handle(EditeToDoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await RepositorioToDoItem.Query().SingleAsync(t => t.Id == request.Id);

                item.Nome = request.Nome;
                item.Descricao = request.Descricao;
                item.DataFim = request.DataFim;
                item.DataInicio = request.DataInicio;
                item.IdToDoList = request.IdToDoList;
                item.Status = Enum.Parse<EnumeradorStatus>(request.Status.ToString());

                RepositorioToDoItem.Atualizar(item);

                await UnidadeDeTrabalho.SalveAlteracoes();
                return CommandResponse();
            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task<UMBITMessageResponse> IRequestHandler<EditeToDoListItemCommand, UMBITMessageResponse>.Handle(EditeToDoListItemCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var item = await RepositorioToDoList.Query().SingleAsync(t => t.Id == request.Id);

                item.Nome = request.Nome;

                RepositorioToDoList.Atualizar(item);

                await UnidadeDeTrabalho.SalveAlteracoes();
                return CommandResponse();
            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task<UMBITMessageResponse> IRequestHandler<FinalizeToDoItemCommand, UMBITMessageResponse>.Handle(FinalizeToDoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var item = await RepositorioToDoItem.Query().SingleAsync(t => t.Id == request.Id);

                item.Status = EnumeradorStatus.Concluido;

                RepositorioToDoItem.Atualizar(item);

                await UnidadeDeTrabalho.SalveAlteracoes();
                return CommandResponse();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
