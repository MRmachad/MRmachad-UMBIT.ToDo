using MediatR;

namespace UMBIT.ToDo.Core.Repositorio.Application.Events.Operacoes
{
    public class CommitTransactionEvent : INotification
    {
        public Guid TransactionId { get; set; }
        public CommitTransactionEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
