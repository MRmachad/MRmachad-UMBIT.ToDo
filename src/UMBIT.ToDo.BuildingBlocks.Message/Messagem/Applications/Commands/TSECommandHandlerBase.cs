using FluentValidation.Results;
using UMBIT.ToDo.Core.Basicos.Excecoes;
using UMBIT.ToDo.Core.Notificacao.Interfaces;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;

namespace UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands
{
    public abstract class UMBITCommandHandlerBase
    {
        private INotificador Notificador;
        private ValidationResult ValidationResult;
        protected IUnidadeDeTrabalhoNaoTransacional UnidadeDeTrabalho;
        public UMBITCommandHandlerBase(
            IUnidadeDeTrabalhoNaoTransacional unidadeDeTrabalho,
            INotificador notificador)
        {
            Notificador = notificador;
            UnidadeDeTrabalho = unidadeDeTrabalho;

            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string propriedade, string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(propriedade, mensagem));
            Notificador.AdicionarNotificacao(propriedade, mensagem);
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
            Notificador.AdicionarNotificacao(string.Empty, mensagem);
        }

        protected UMBITMessageResponse CommandResponse()
        {
            try
            {
                var res = new UMBITMessageResponse(ValidationResult);
                return res;
            }
            catch (Exception ex)
            {
                var type = GetType().Name;
                throw new ExcecaoBasicaUMBIT($"Falha na execução de comando {type}!", ex);
            }
        }
        protected UMBITMessageResponse<T> CommandResponse<T>(T? response = null)
            where T : class
        {
            try
            {
                var queryResponse = new UMBITMessageResponse<T>(ValidationResult);
                queryResponse.SetDados(response);

                return queryResponse;
            }
            catch (Exception ex)
            {
                var type = GetType().Name;
                throw new ExcecaoBasicaUMBIT($"Falha na execução de comando {type}!", ex);
            }
        }
        protected Task<UMBITMessageResponse> TaskCommandResponse()
        {
            try
            {
                var res = new UMBITMessageResponse(ValidationResult);
                return Task.FromResult(res);
            }
            catch (Exception ex)
            {
                var type = GetType().Name;
                throw new ExcecaoBasicaUMBIT($"Falha na execução de comando {type}!", ex);
            }
        }
        protected Task<UMBITMessageResponse<T>> TaskCommandResponse<T>(T? response = null)
            where T : class
        {
            try
            {
                var queryResponse = new UMBITMessageResponse<T>(ValidationResult);
                queryResponse.SetDados(response);

                return Task.FromResult(queryResponse);
            }
            catch (Exception ex)
            {
                var type = GetType().Name;
                throw new ExcecaoBasicaUMBIT($"Falha na execução de comando {type}!", ex);
            }
        }
    }
}
