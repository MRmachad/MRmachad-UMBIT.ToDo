using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using UMBIT.Nexus.Auth.Dominio.Basicos;
using UMBIT.ToDo.Core.Messages.Messagem;
using UMBIT.ToDo.Core.Messages.Messagem.Applications.Commands;
using UMBIT.ToDo.Core.Messages.Messagem.Interfaces;
using UMBIT.ToDo.Core.Notificacao.Interfaces;
using UMBIT.ToDo.Core.Repositorio.Interfaces.Database;
using UMBIT.ToDo.Core.Seguranca.Models;
using UMBIT.ToDo.Dominio.Application.Commands.Usuarios;
using UMBIT.ToDo.Dominio.Configuradores;
using UMBIT.ToDo.Dominio.Entidades.Basicos;

namespace UMBIT.ToDo.Dominio.Application.Commands
{
    public class UsuariosCommandHandler : UMBITCommandHandlerBase,
        IUMBITCommandRequestHandler<AtualizarUsuarioCommand>,
        IUMBITCommandRequestHandler<AtualizarDadosDeUsuarioCommand>,
        IUMBITCommandRequestHandler<AprovarAlteracaoSenhaCommand>,
        IUMBITCommandRequestHandler<RemoverUsuarioCommand>
    {
        private readonly ContextoPrincipal _contextoPrincipal;
        private readonly UserManager<Usuario> _userManager;
        private readonly IOptions<IdentitySettings> _identitySettings;

        public UsuariosCommandHandler(
            IUnidadeDeTrabalho unidadeDeTrabalho,
            INotificador notificador,
            ContextoPrincipal contextoPrincipal,
            UserManager<Usuario> userManager,
            IOptions<IdentitySettings> identitySettings) : base(unidadeDeTrabalho, notificador)
        {
            _contextoPrincipal = contextoPrincipal;
            _userManager = userManager;
            _identitySettings = identitySettings;
        }

        public async Task<UMBITMessageResponse> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByIdAsync(request.IdUsuario.ToString());
            if (usuario == null)
            {
                AdicionarErro("Usuário não encontrado");
                return CommandResponse();
            }

            usuario.Nome = request.NomeUsuario;

            var result = await _userManager.UpdateAsync(usuario);

            if (!result.Succeeded)
                AdicionarErro("Falha durante a atualização do usuário");

            return CommandResponse();
        }

        public async Task<UMBITMessageResponse> Handle(AtualizarDadosDeUsuarioCommand request, CancellationToken cancellationToken)
        {
            var principal = _contextoPrincipal.ObtenhaPrincipal();

            if (principal == null)
            {
                AdicionarErro("Usuáiro não está autenticado");
                return CommandResponse();
            }

            var usuario = await _userManager.FindByIdAsync(principal.Id);

            if (usuario == null || !(string.Compare(usuario.Email, request.Email, StringComparison.OrdinalIgnoreCase) == 0))
            {
                AdicionarErro("Usuário não encontrado");
                return CommandResponse();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            usuario.Nome = request.Nome;

            await _userManager.ResetPasswordAsync(usuario, token, request.Senha);
            await _userManager.UpdateAsync(usuario);

            return CommandResponse();
        }

        public async Task<UMBITMessageResponse> Handle(RemoverUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByIdAsync(request.IdUsuario.ToString());

            if (usuario == null)
            {
                AdicionarErro("Usuário não encontrado");
                return CommandResponse();
            }

            var claims = await _userManager.GetClaimsAsync(usuario);
            if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == TipoUsuario.ADMINISTRADOR))
            {
                AdicionarErro("Não é possível remover o usuário administrador do sistema");
                return CommandResponse();
            }

            var result = await _userManager.DeleteAsync(usuario);

            if (!result.Succeeded)
                AdicionarErro("Falha ao remover usuário.");

            return CommandResponse();
        }

        public async Task<UMBITMessageResponse> Handle(AprovarAlteracaoSenhaCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByIdAsync(request.UsuarioId.ToString());

            if (usuario == null)
            {
                AdicionarErro("Usuaário inválido");
                return CommandResponse();
            }

            if (!usuario.AtualizacaoSenhaRequisitada)
            {
                AdicionarErro("Usuário não solicitou alteração de senha.");
                return CommandResponse();
            }

            usuario.AprovarAtualizacaoDeSenha();
            var result = await _userManager.UpdateAsync(usuario);

            if (!result.Succeeded)
                AdicionarErro("Falha durante a atualização do usuário");

            return CommandResponse();
        }
    }
}
