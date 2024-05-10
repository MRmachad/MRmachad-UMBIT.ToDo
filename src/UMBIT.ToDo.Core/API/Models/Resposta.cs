using Newtonsoft.Json;
using UMBIT.ToDo.SDK.Notificacao;

namespace UMBIT.ToDo.SDK.API.Models
{
    public class Resposta
    {
        [JsonProperty("sucesso")]
        public bool Sucesso { get; set; }

        [JsonProperty("dados")]
        public object Dados { get; set; }

        [JsonProperty("erros")]
        public IEnumerable<Notificacao.Notificacao> Erros { get; set; }

        [JsonProperty("erros_sistema")]
        public IEnumerable<ErroSistema> ErrosSistema { get; set; }
    }
}
