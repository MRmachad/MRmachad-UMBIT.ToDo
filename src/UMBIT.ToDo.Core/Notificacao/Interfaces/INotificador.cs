using FluentValidation.Results;
using UMBIT.ToDo.Core.Basicos.Notificacoes;

namespace UMBIT.ToDo.Core.Notificacao.Interfaces
{
    public interface INotificador
    {
        IEnumerable<NotificacaoPadrao> ObterTodos();
        IEnumerable<NotificacaoPadrao> ObterNotificacoes();
        IEnumerable<ErroSistema> ObterErrosSistema();
        void AdicionarNotificacao(string mensagem);
        void AdicionarNotificacao(string titulo, string mensagem);
        void AdicionarNotificacao(ValidationResult validationResult);
        void AdicionarErroSistema(ErroSistema erroSistema);
        bool TemNotificacoes();
        void LimparNotificacoes();
    }
}
