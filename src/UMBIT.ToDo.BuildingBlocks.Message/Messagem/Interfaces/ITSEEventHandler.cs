using MediatR;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Events;
using UMBIT.ToDo.Core.Repositorio.Application.Events.Operacoes.Base;

namespace UMBIT.ToDo.Core.Messages.Messagem.Interfaces
{
    public interface IUMBITEventHandler<T> : INotificationHandler<T> where T : UMBITEvent
    {
    }
    public interface IUMBITDomainEventHandler<T> : INotificationHandler<T> where T : DomainEvent
    {
    }
}
