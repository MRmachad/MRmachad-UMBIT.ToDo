using UMBIT.ToDo.Core.Basicos.Enumeradores;
using UMBIT.ToDo.Core.Seguranca.Interfaces;

namespace UMBIT.ToDo.Core.Seguranca.Basicos.Enumeradores
{
    public abstract class EnumeradorDePermissaoBase<T> : EnumBase<T>, IPermissao where T : Enum
    {
        public string Assembly { get; private set; }
        public string IdentificadorCompleto => $"{Assembly}.{Nome}";
        public EnumeradorDePermissaoBase(T @enum, string descricao) : base(@enum, descricao)
        {
            Assembly = GetType().Assembly.GetName().Name ?? string.Empty;
        }
    }
}
