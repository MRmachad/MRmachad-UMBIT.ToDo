using MediatR;

namespace UMBIT.ToDo.Core.Repositorio.Application.Events.Operacoes
{
    public class BeginTransactionEvent : INotification
    {
        public Guid TransactionId { get; set; }
        public BeginTransactionEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
