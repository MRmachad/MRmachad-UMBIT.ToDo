using UMBIT.ToDo.Core.API.Models;
using UMBIT.ToDo.Core.Basicos.Excecoes;

namespace UMBIT.ToDo.BuildingBlocksc.ASPNet.Basicos.Exececoes
{
    public class ExcecaoServicoExterno : ExcecaoBasicaUMBIT
    {
        public Resposta? APIReposta { get; set; }
        public ExcecaoServicoExterno(string mensagem, Resposta? resposta = null) : base(mensagem)
        {
            APIReposta = resposta;
        }
    }
}
