using MediatR;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;

namespace UMBIT.ToDo.Core.Messages.Messagem.Interfaces
{
    public interface IUMBITActionHandler<T> : INotificationHandler<T> where T : IUMBITAction
    {
    }
}
