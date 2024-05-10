using System;

namespace UMBIT.ToDo.SDK.Entidades
{
    public abstract class BaseEntity
    {
        public Guid Id { get;  set; } 
        public DateTime DataCriacao { get;  set; }
        public DateTime DataAtualizacao { get;  set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
