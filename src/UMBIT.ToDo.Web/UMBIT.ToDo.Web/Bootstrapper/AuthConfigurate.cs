using System.Text.Json;
using UMBIT.Nexus.Auth.Contrato;
using UMBIT.ToDo.Web.Models;

namespace UMBIT.ToDo.Web.Bootstrapper
{
    public static class AuthConfigurate
    {
        public static IServiceCollection AddAuthSession(this IServiceCollection services)
        {
            services.AddScoped<AuthSessionContext>();
            return services;
        }
        public class AuthSessionContext
        {
            public const string AUTH_CONTEXT = "AUTH_CONTEXT";

            public bool EhAutenticado => !String.IsNullOrEmpty(httpContextAccessor.HttpContext?.Session.GetString(AUTH_CONTEXT));
            public IHttpContextAccessor httpContextAccessor { get; set; }

            public AuthSessionContext(IHttpContextAccessor httpContextAccessor)
            {
                this.httpContextAccessor = httpContextAccessor;
            }

            public void SetAuthContext(TokenResponseDTO tokenResponseDTO)
            {
                httpContextAccessor.HttpContext?.Session.SetString(AUTH_CONTEXT, JsonSerializer.Serialize(tokenResponseDTO));
            }

            public void RemoveAuthContext()
            {
                httpContextAccessor.HttpContext?.Session.Remove(AUTH_CONTEXT);
            }

            public TokenResponseDTO? GetAuthContext()
            {
                var jPrincipal = httpContextAccessor.HttpContext?.Session.GetString(AUTH_CONTEXT);

                if (String.IsNullOrEmpty(jPrincipal)) { return null; }

                return JsonSerializer.Deserialize<TokenResponseDTO>(jPrincipal);
            }
        }
    }
}
