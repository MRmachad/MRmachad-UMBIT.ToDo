using MediatR;
using UMBIT.ToDo.Core.Messages.Messagem;

namespace UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands
{
    public interface IUMBITAction : INotification
    {

    }
    public class UMBITAction : UMBITMensagem, IUMBITAction
    {
    }
}
