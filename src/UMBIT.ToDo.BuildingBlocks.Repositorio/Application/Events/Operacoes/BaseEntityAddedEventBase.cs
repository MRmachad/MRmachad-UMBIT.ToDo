using UMBIT.ToDo.Core.Repositorio.Application.Events.Operacoes.Base;
using UMBIT.ToDo.Core.Repositorio.Interfaces;

namespace UMBIT.ToDo.Core.Repositorio.Application.Events.Operacoes
{
    public class BaseEntityAddedEventBase<T> : DomainEvent where T : class
    {
        public BaseEntityAddedEventBase()
        {

        }
        public BaseEntityAddedEventBase(IBaseEntity data)
        {
            Data = (data as T)!;
        }

        public T Data { get; set; }

        public override string ObtenhaChaveDeComunicacao()
        {
            return $"{typeof(T).Name}.Added";
        }
    }
}
