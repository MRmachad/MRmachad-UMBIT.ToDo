﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UMBIT.ToDo.Core.Messages.Messagem;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;
using UMBIT.ToDo.Core.Messages.Messagem.Interfaces;
using UMBIT.ToDo.Core.Notificacao.Interfaces;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;
using UMBIT.ToDo.Dominio.Application.Queries.Configuracao;
using UMBIT.ToDo.Dominio.Entidades.Basicos;
using UMBIT.ToDo.Dominio.Entidades.Configuracao;

namespace UMBIT.ToDo.Dominio.Application.Queries
{
    public class ConfiguracaoQueryHandler :
        UMBITQueryHandlerBase,
        IUMBITQueryRequestHandler<ObterStatusDeConfiguracaoQuery, StatusDeConfiguracao>
    {
        private readonly UserManager<Usuario> _userManager;
        public ConfiguracaoQueryHandler(
            IUnidadeDeTrabalhoDeLeitura unidadeDeTrabalho,
            INotificador notificador,
            UserManager<Usuario> userManager) : base(unidadeDeTrabalho, notificador)
        {
            _userManager = userManager;
        }

        public async Task<UMBITMessageResponse<StatusDeConfiguracao>> Handle(ObterStatusDeConfiguracaoQuery request, CancellationToken cancellationToken)
        {
            var configured = await _userManager.Users.AnyAsync();
            return QueryResponse(new StatusDeConfiguracao(configured));
        }
    }
}