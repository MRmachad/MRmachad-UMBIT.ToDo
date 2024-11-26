using MediatR;

namespace UMBIT.ToDo.Core.Repositorio.Application.Events.Operacoes
{
    public class RollbackTransactionEvent : INotification
    {
        public Guid TransactionId { get; set; }
        public RollbackTransactionEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
