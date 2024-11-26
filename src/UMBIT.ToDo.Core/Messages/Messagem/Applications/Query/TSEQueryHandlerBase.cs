using FluentValidation.Results;
using UMBIT.ToDo.Core.Basicos.Excecoes;
using UMBIT.ToDo.Core.Notificacao.Interfaces;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;

namespace UMBIT.ToDo.Core.Messages.Messagem.Applications.Query
{
    public abstract class UMBITQueryHandlerBase
    {
        private ValidationResult ValidationResult;
        protected IUnidadeDeTrabalhoDeLeitura UnidadeDeTrabalho;

        private INotificador Notificador;

        public UMBITQueryHandlerBase(
            IUnidadeDeTrabalhoDeLeitura unidadeDeTrabalho,
            INotificador notificador)
        {
            UnidadeDeTrabalho = unidadeDeTrabalho;
            Notificador = notificador;

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

        protected UMBITMessageResponse<T> QueryResponse<T>(T? response = null)
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
        protected Task<UMBITMessageResponse<T>> TaskQueryResponse<T>(T? response = null)
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
