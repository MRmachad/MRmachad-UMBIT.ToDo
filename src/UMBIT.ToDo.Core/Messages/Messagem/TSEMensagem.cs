namespace UMBIT.ToDo.Core.Messages.Messagem
{
    [Serializable]
    public abstract class UMBITMensagem
    {
        public Guid AggregateId { get; protected set; }
        public string TipoMensagem { get; protected set; }

        protected UMBITMensagem()
        {
            AggregateId = Guid.NewGuid();
            TipoMensagem = GetType().Name;
        }
    }
}
