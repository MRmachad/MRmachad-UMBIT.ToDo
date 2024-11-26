using UMBIT.ToDo.Core.Notificacao.Interfaces;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;

namespace UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands
{
    public abstract class UMBITActionHandlerBase
    {
        protected IUnidadeDeTrabalhoNaoTransacional UnidadeDeTrabalho;
        private INotificador Notificador;
        public UMBITActionHandlerBase(
            IUnidadeDeTrabalhoNaoTransacional unidadeDeTrabalho,
            INotificador notificador)
        {
            Notificador = notificador;
            UnidadeDeTrabalho = unidadeDeTrabalho;
        }

        protected void AdicionarErro(string propriedade, string mensagem)
        {
            Notificador.AdicionarNotificacao(propriedade, mensagem);
        }

        protected void AdicionarErro(string mensagem)
        {
            Notificador.AdicionarNotificacao(string.Empty, mensagem);
        }
    }
}
