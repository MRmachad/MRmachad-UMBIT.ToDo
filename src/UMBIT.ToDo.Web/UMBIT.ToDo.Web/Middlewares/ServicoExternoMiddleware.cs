using Azure;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using UMBIT.ToDo.Core.API.Models;
using UMBIT.ToDo.Core.Basicos.Excecoes;
using UMBIT.ToDo.Core.Seguranca.Models;
using UMBIT.ToDo.Fabrica.Models;

namespace UMBIT.ToDo.Web.Middlewares
{
    public class ServicoExternoMiddleware : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ServicoExternoMiddleware(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            TrateRequest(request);

            return TrateResposta(base.Send(request, cancellationToken));
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            TrateRequest(request);
            return TrateResposta(await base.SendAsync(request, cancellationToken));
        }

        private HttpResponseMessage TrateResposta(HttpResponseMessage response)
        {
            try
            {

                var stringResposta = response?.Content?.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode && TenteRepostaPadrao(stringResposta, out Resposta respotaPadrao))
                {
                    if (!respotaPadrao.Sucesso)
                    {

                        var ex = new ExcecaoBasicaUMBIT(
                                                    respotaPadrao.Erros?.FirstOrDefault()?.Titulo ?? "Falha Generica, respota padrão sem erros!",
                                                    new Exception(string.Join("\n", respotaPadrao.ErrosSistema.Select(t => t.Mensagem))));

                        throw ex;
                    }

                    var contentString = System.Text.Json.JsonSerializer.Serialize(respotaPadrao.Dados);
                    response.Content = new StringContent(contentString, Encoding.UTF8, "application/json");
                    return response;

                }
                else if (response.IsSuccessStatusCode)
                    return TenteRepostaComum(response, stringResposta);

                throw new ExcecaoBasicaUMBIT($"Falha no uso do serviço, {response.RequestMessage?.RequestUri?.AbsolutePath} respondeu {response.ReasonPhrase}.");
            }
            catch (Exception ex)
            {
                throw new ExcecaoBasicaUMBIT($"Erro no uso do serviço, algo de errado aconteceu em {response.RequestMessage?.RequestUri?.AbsolutePath}.", ex);
            }
        }

        private HttpRequestMessage TrateRequest(HttpRequestMessage request)
        {
            var acessToken = this._contextAccessor.HttpContext?.Session.GetString("AccessToken");
            if (!String.IsNullOrEmpty(acessToken))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", acessToken);

            return request;
        }

        private bool TenteRepostaPadrao(string httpResponseMessage, out Resposta resposta)
        {
            try
            {
                resposta = JsonSerializer.Deserialize<Resposta>(httpResponseMessage);
                return resposta != null;
            }
            catch { resposta = null; return false; }
        }
        private HttpResponseMessage TenteRepostaComum(HttpResponseMessage httpResponseMessage, string stringResposta)
        {
            try
            {
                httpResponseMessage.Content = new StringContent(stringResposta, Encoding.UTF8, "application/json");
                return httpResponseMessage;
            }

            catch { return null; }
        }

    }
}
