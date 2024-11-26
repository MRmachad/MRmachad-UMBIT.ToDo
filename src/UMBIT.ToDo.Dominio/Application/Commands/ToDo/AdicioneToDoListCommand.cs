using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;

namespace UMBIT.ToDo.Dominio.Application.Commands.ToDo
{
    public class AdicioneToDoListCommand : UMBITCommand<AdicioneToDoListCommand>
    {
        public string Nome { get; set; }
        public List<(string Nome, string? Descricao, DateTime DataInicio, DateTime DataFim, int Status)>? Items { get;set; }
        protected override void Validadors(ValidatorCommand<AdicioneToDoListCommand> validator)
        {
        }
    }
}
