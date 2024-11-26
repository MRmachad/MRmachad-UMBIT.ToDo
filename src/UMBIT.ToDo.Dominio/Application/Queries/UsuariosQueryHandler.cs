﻿using Microsoft.AspNetCore.Identity;
using UMBIT.ToDo.Core.Messages.Messagem;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Query;
using UMBIT.ToDo.Core.Messages.Messagem.Interfaces;
using UMBIT.ToDo.Core.Notificacao.Interfaces;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;
using UMBIT.ToDo.Dominio.Application.Queries.Usuarios;
using UMBIT.ToDo.Dominio.Entidades.Basicos;

namespace UMBIT.ToDo.Dominio.Application.Queries
{
    public class UsuariosQueryHandler : UMBITQueryHandlerBase,
        IUMBITQueryRequestHandler<ObterUsuariosQuery, IQueryable<Usuario>>,
        IUMBITQueryRequestHandler<ObterUsuarioPorIdQuery, Usuario>,
        IUMBITQueryRequestHandler<ObterUsuarioPorEmailQuery, Usuario>
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuariosQueryHandler(
            IUnidadeDeTrabalhoDeLeitura unidadeDeTrabalho,
            INotificador notificador,
            UserManager<Usuario> userManager) : base(unidadeDeTrabalho, notificador)
        {
            _userManager = userManager;
        }

        public Task<UMBITMessageResponse<IQueryable<Usuario>>> Handle(ObterUsuariosQuery request, CancellationToken cancellationToken)
        {
            var users = TaskQueryResponse(_userManager.Users);
            return TaskQueryResponse(_userManager.Users);
        }

        public async Task<UMBITMessageResponse<Usuario>> Handle(ObterUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByIdAsync(request.Id.ToString());
            return QueryResponse(usuario);
        }

        public async Task<UMBITMessageResponse<Usuario>> Handle(ObterUsuarioPorEmailQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            return QueryResponse(usuario);
        }

    }
}