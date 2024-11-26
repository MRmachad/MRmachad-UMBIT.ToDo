using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;

namespace UMBIT.ToDo.Dominio.Application.Commands.ToDo
{
    public class AdicioneToDoItemCommand : UMBITCommand<AdicioneToDoItemCommand>
    {
        public Guid? IdToDoList { get; set; }
        public int Index { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int Status { get; set; }
        protected override void Validadors(ValidatorCommand<AdicioneToDoItemCommand> validator)
        {
            throw new NotImplementedException();
        }
    }
}
