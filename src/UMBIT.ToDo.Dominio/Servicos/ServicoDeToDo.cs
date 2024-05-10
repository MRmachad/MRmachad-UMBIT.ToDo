using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMBIT.ToDo.SDK.Basicos.Excecoes;
using UMBIT.ToDo.SDK.Notificacao.Interfaces;
using UMBIT.ToDo.SDK.Repositorio.Interfaces.Database;
using UMBIT.ToDo.SDK.Repositorio.Servicos;
using UMBIT.ToDo.Dominio.Basicos.Enum;
using UMBIT.ToDo.Dominio.Entidades;
using UMBIT.ToDo.Dominio.Interfaces;

namespace UMBIT.ToDo.Dominio.Servicos
{
    public class ServicoDeToDo : ServicoBase, IServicoDeToDo
    {
        private readonly INotificador Notificador;
        private readonly IRepositorio<ToDoItem> RepositorioToDoItem;
        private readonly IRepositorio<ToDoList> RepositorioToDoList;


        public ServicoDeToDo(IUnidadeDeTrabalho unidadeDeTrabalho, INotificador notificador) : base(unidadeDeTrabalho)
        {
            this.Notificador = notificador;
            this.RepositorioToDoList = this.UnidadeDeTrabalho.ObterRepositorio<ToDoList>();
            this.RepositorioToDoItem = this.UnidadeDeTrabalho.ObterRepositorio<ToDoItem>();
        }

        public async Task AdicioneToDoItem(ToDoItem toDoItem)
        {
            await this.UnidadeDeTrabalho.InicieTransacao();
            try
            {
                await this.RepositorioToDoItem.Adicionar(toDoItem);
                await this.UnidadeDeTrabalho.SalveAlteracoes();
                await this.UnidadeDeTrabalho.FinalizeTransacao();
            }
            catch (Exception ex)
            {

                await this.UnidadeDeTrabalho.RevertaTransacao();
                this.Notificador.AdicionarErroSistema(new SDK.Notificacao.ErroSistema("Erro ao Adicionar item", ex));
            }
        }

        public async Task AdicioneToDoList(string nome, List<ToDoItem> toDoItems)
        {
            await this.UnidadeDeTrabalho.InicieTransacao();
            try
            {
                var toDolist = new ToDoList();
                toDolist.Id = Guid.NewGuid();
                toDolist.Nome = nome;
                await this.RepositorioToDoList.Adicionar(toDolist);

                if (toDoItems != null)
                    foreach (var toDoItem in toDoItems)
                    {
                        toDoItem.IdToDoList = toDolist.Id;
                        await this.RepositorioToDoItem.Adicionar(toDoItem);
                    }

                await this.UnidadeDeTrabalho.SalveAlteracoes();
                await this.UnidadeDeTrabalho.FinalizeTransacao();
            }
            catch (Exception ex)
            {

                await this.UnidadeDeTrabalho.RevertaTransacao();
                this.Notificador.AdicionarErroSistema(new SDK.Notificacao.ErroSistema("Erro ao Adicionar lista", ex));
            }
        }

        public async Task DeleteToDoItem(Guid id)
        {
            await this.UnidadeDeTrabalho.InicieTransacao();
            try
            {
                var item = await this.RepositorioToDoItem.ObterUnico(id);
                this.RepositorioToDoItem.Remover(item);

                await this.UnidadeDeTrabalho.SalveAlteracoes();
                await this.UnidadeDeTrabalho.FinalizeTransacao();
            }
            catch (Exception ex)
            {

                await this.UnidadeDeTrabalho.RevertaTransacao();
                this.Notificador.AdicionarErroSistema(new SDK.Notificacao.ErroSistema("Erro ao Deletar item", ex));
            }
        }

        public async Task DeleteToDoList(Guid id)
        {
            await this.UnidadeDeTrabalho.InicieTransacao();
            try
            {
                var list = await this.RepositorioToDoList.ObterUnico(id);
                this.RepositorioToDoList.Remover(list);

                await this.UnidadeDeTrabalho.SalveAlteracoes();
                await this.UnidadeDeTrabalho.FinalizeTransacao();
            }
            catch (Exception ex)
            {

                await this.UnidadeDeTrabalho.RevertaTransacao();
                this.Notificador.AdicionarErroSistema(new SDK.Notificacao.ErroSistema("Erro ao Deletar lista", ex));
            }
        }

        public async Task EditeToDoItem(Guid id, Guid? idToDoList,DateTime datafim, DateTime datainicio, string nome, string descricao, int status)
        {
            await this.UnidadeDeTrabalho.InicieTransacao();
            try
            {
                var item = await this.RepositorioToDoItem.ObterUnico(id);

                item.Nome = nome;
                item.Descricao = descricao;
                item.DataFim = datafim;
                item.DataInicio = datainicio;
                item.IdToDoList = idToDoList;
                item.Status = EnumeradorStatus.Parse<EnumeradorStatus>(status.ToString());

                this.RepositorioToDoItem.Atualizar(item);

                await this.UnidadeDeTrabalho.SalveAlteracoes();
                await this.UnidadeDeTrabalho.FinalizeTransacao();
            }
            catch (Exception ex)
            {

                await this.UnidadeDeTrabalho.RevertaTransacao();
                this.Notificador.AdicionarErroSistema(new SDK.Notificacao.ErroSistema("Erro ao Editar item", ex));
            }
        }

        public async Task EditeToDoListItem(Guid id, string nome)
        {
            await this.UnidadeDeTrabalho.InicieTransacao();
            try
            {
                var item = await this.RepositorioToDoList.ObterUnico(id);

                item.Nome = nome;

                this.RepositorioToDoList.Atualizar(item);

                await this.UnidadeDeTrabalho.SalveAlteracoes();
                await this.UnidadeDeTrabalho.FinalizeTransacao();
            }
            catch (Exception ex)
            {

                await this.UnidadeDeTrabalho.RevertaTransacao();
                this.Notificador.AdicionarErroSistema(new SDK.Notificacao.ErroSistema("Erro ao Editar lista", ex));
            }
        }

        public async Task FinalizeToDoItem(Guid id)
        {
            await this.UnidadeDeTrabalho.InicieTransacao();
            try
            {

                var item = await this.RepositorioToDoItem.ObterUnico(id);

                item.Status = Basicos.Enum.EnumeradorStatus.Concluido;

                this.RepositorioToDoItem.Atualizar(item);

                await this.UnidadeDeTrabalho.SalveAlteracoes();
                await this.UnidadeDeTrabalho.FinalizeTransacao();
            }
            catch (Exception ex)
            {

                await this.UnidadeDeTrabalho.RevertaTransacao();
                this.Notificador.AdicionarErroSistema(new SDK.Notificacao.ErroSistema("Erro ao finalizar item", ex));
            }
        }

        public async Task<ToDoItem> ObtenhaToDoItem(Guid id)
        {
            try
            {
                return await this.RepositorioToDoItem.ObterUnico(id);

            }
            catch (Exception ex)
            {
                throw new ExcecaoBasicaUMBIT("Erro ao obter Item", ex);
            }
        }

        public async Task<IEnumerable<ToDoItem>> ObtenhaToDoItems()
        {
            try
            {
                return await this.RepositorioToDoItem.ObterTodos();
            }
            catch (Exception ex)
            {
                throw new ExcecaoBasicaUMBIT("Erro ao obter Itens", ex);
            }
        }

        public async Task<ToDoList> ObtenhaToDoList(Guid id)
        {
            try
            {
                return await this.RepositorioToDoList.ObterUnico(id);
            }
            catch (Exception ex)
            {
                throw new ExcecaoBasicaUMBIT("Erro ao obter Lista", ex);
            }
        }

        public async Task<IEnumerable<ToDoList>> ObtenhaToDoLists()
        {
            try
            {
                return await this.RepositorioToDoList.ObterTodos();
            }
            catch (Exception ex)
            {
                throw new ExcecaoBasicaUMBIT("Erro ao obter Listas", ex);
            }
        }
    }
}
