using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UMBIT.ToDo.Core.Seguranca.Interfaces;

namespace UMBIT.ToDo.Core.Seguranca.Models
{
    public class ContextoPrincipal
    {
        private const string BEARER_SCHEME = "Bearer ";
        private readonly HttpContext HttpContext;

        public string? BearerToken = string.Empty;
        public bool EhAutenticado => HttpContext?.User?.Identity != null ?
            HttpContext.User.Identity.IsAuthenticated : false;

        public ContextoPrincipal(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
            BearerToken = httpContextAccessor.HttpContext?.Request?.Headers["Authorization"].FirstOrDefault(t => t!.StartsWith(BEARER_SCHEME))?.Substring(BEARER_SCHEME.Length).Trim();
        }

        public ContextoPrincipal(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        public Principal? ObtenhaPrincipal()
        {
            if (HttpContext?.User == null)
                return null;

            var _user = HttpContext.User;

            return new Principal(_user);
        }
    }

    public class Principal
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public List<Claim> Claims { get; private set; }

        public Principal(ClaimsPrincipal userPrincipal)
        {

            Id = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            Email = userPrincipal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            Claims = userPrincipal.Claims.ToList();
        }

        public bool PossuiPermissaoDeAcesso(IPermissao enumeradorDePermissaoBase)
        {
            return EhAdministrador() ||
                Claims.Any(c =>
                    c.Type == ClaimTypes.Role &&
                    string.Compare(c.Value, enumeradorDePermissaoBase.IdentificadorCompleto, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public bool EhAdministrador()
        {
            return Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrador");
        }

        public bool EhValido()
        {
            return
                !string.IsNullOrEmpty(Id) &&
                !string.IsNullOrEmpty(Email);
        }

    }
}
