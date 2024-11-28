using UMBIT.ToDo.Core.Messages.Messagem;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Events;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;

namespace UMBIT.ToDo.Core.Messages.Bus.MediatorBus
{
    public interface IMediatorBus
    {
        void PublicarEvento<TEvent>(TEvent evento)
            where TEvent : UMBITEvent;

        Task<UMBITMessageResponse> EnviarComando<TCommand>(TCommand comando)
            where TCommand : class, IUMBITCommandRequest<TCommand>;

        Task<UMBITMessageResponse<TResp>> EnviarComando<TCommand, TResp>(TCommand comando)
            where TCommand : class, IUMBITCommandRequest<TCommand, TResp>
            where TResp : class;

        Task<UMBITMessageResponse<TResp>> EnviarQuery<TQuery, TResp>(TQuery comando)
            where TQuery : IUMBITQuery<TResp>
            where TResp : class;
    }
}
