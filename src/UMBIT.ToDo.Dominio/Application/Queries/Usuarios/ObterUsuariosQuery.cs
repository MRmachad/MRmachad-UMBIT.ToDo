﻿using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;
using UMBIT.ToDo.Dominio.Entidades.Basicos;

namespace UMBIT.ToDo.Dominio.Application.Queries.Usuarios
{
    public class ObterUsuariosQuery : UMBITQuery<ObterUsuariosQuery, IQueryable<Usuario>>
    {
        protected override void Validadors(ValidatorQuery<ObterUsuariosQuery> validator)
        {
        }
    }
}