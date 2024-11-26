namespace UMBIT.ToDo.Dominio.Entidades.Configuracao
{
    public class StatusDeConfiguracao
    {
        public bool Configurado { get; private set; }

        public StatusDeConfiguracao(bool configurado)
        {
            Configurado = configurado;
        }
    }
}
