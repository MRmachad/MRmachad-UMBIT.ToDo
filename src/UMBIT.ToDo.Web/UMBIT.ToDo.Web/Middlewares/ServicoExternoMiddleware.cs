using System.Text;
using UMBIT.ToDo.Core.API.Models;
using UMBIT.ToDo.Core.Basicos.Excecoes;

namespace UMBIT.ToDo.Web.Middlewares
{
    public class ServicoExternoMiddleware : DelegatingHandler
    {


        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return TrateResposta(base.Send(request, cancellationToken));
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return TrateResposta(await base.SendAsync(request, cancellationToken));
        }

        private HttpResponseMessage TrateResposta(HttpResponseMessage response)
        {
            try
            {
                if (response.IsSuccessStatusCode && TenteRepostaPadrao(response, out Resposta respotaPadrao))
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
                    return response;

                throw new ExcecaoBasicaUMBIT($"Falha no uso do serviço, {response.RequestMessage?.RequestUri?.AbsolutePath} respondeu {response.ReasonPhrase}.");
            }
            catch (Exception ex)
            {
                throw new ExcecaoBasicaUMBIT($"Erro no uso do serviço, algo de errado aconteceu em {response.RequestMessage?.RequestUri?.AbsolutePath}.", ex);
            }
        }

        private bool TenteRepostaPadrao(HttpResponseMessage httpResponseMessage, out Resposta resposta)
        {
            try
            {
                resposta = httpResponseMessage?.Content?.ReadFromJsonAsync<Resposta>().Result;
                return resposta != null;
            }
            catch { resposta = null; return false; }
        }

    }
}
