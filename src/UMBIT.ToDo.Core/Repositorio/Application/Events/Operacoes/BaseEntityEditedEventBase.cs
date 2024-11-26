using UMBIT.ToDo.Core.Repositorio.Application.Events.Operacoes.Base;
using UMBIT.ToDo.Core.Repositorio.Interfaces;

namespace UMBIT.ToDo.Core.Repositorio.Application.Events.Operacoes
{
    public class BaseEntityEditedEventBase<T> : DomainEvent where T : class
    {
        public BaseEntityEditedEventBase()
        {

        }
        public BaseEntityEditedEventBase(IBaseEntity dataOrigin, IBaseEntity dataEdited)
        {
            DataOrigin = (dataOrigin as T)!;
            DataEdited = (dataEdited as T)!;
        }

        public T DataOrigin { get; set; }
        public T DataEdited { get; set; }
        public override string ObtenhaChaveDeComunicacao()
        {
            return $"{typeof(T).Name}.Edited";
        }
    }
}
