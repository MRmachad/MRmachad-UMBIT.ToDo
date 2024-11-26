using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;

namespace UMBIT.ToDo.Dominio.Application.Commands.ToDo
{
    internal class FinalizeToDoItemCommand : UMBITCommand<FinalizeToDoItemCommand>
    {
        public Guid Id { get; set; }    
        protected override void Validadors(ValidatorCommand<FinalizeToDoItemCommand> validator)
        {
            throw new NotImplementedException();
        }
    }
}
