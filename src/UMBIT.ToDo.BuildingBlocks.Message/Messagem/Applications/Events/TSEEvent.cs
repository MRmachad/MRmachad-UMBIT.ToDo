using MediatR;
using UMBIT.ToDo.Core.Messages.Messagem;

namespace UMBIT.ToDo.Core.Messages.Messagem.Applications.Events
{
    [Serializable]
    public abstract class UMBITEvent : UMBITMensagem, INotification
    {
    }
}
