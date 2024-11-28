using MediatR;
using UMBIT.ToDo.Core.Messages.Messagem;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;

namespace UMBIT.ToDo.Core.Messages.Messagem.Interfaces
{
    public interface IUMBITQueryRequestHandler<TQuery, T> : IRequestHandler<TQuery, UMBITMessageResponse<T>>
        where T : class
        where TQuery : IUMBITQuery<T>
    {
    }
}
