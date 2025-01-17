﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMBIT.ToDo.SDK.Repositorio.Servicos
{
    public interface IServicoBase<T> where T : class
    {
        Task<IEnumerable<T>> Obter();
        Task<T> ObterUnico(Guid id);
        Task Adicionar(T objeto);
        Task Adicionar(IEnumerable<T> objetos);
        Task Atualize(T objeto);
        Task Remover(Guid id);
    }
}
