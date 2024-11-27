using Microsoft.AspNetCore.Mvc;
using UMBIT.Nexus.Auth.Contrato;
using UMBIT.ToDo.Web.services;
using static UMBIT.ToDo.Web.Bootstrapper.AuthConfigurate;

namespace UMBIT.ToDo.Web.Controllers
{
    public class AuthController : Controller
    {
        private IServicoAuth _serviceAuth { get; set; }
        private AuthSessionContext _authSessionContext { get; set; }
        public AuthController(IServicoAuth serviceAuth , AuthSessionContext authSessionContext)
        {
            _serviceAuth = serviceAuth;
            _authSessionContext = authSessionContext;
        }
        public async Task<IActionResult> Login()
        {
            if (!(await _serviceAuth.CheckAuth()).Configured)
            {
                return RedirectToAction(nameof(CreateAdministrator));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            loginRequestDTO.Audience = "web";
            var tokenResponse = await _serviceAuth.Login(loginRequestDTO);

            _authSessionContext.SetAuthContext(tokenResponse);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            var tokenResponse = await _serviceAuth.Logout();

            _authSessionContext.RemoveAuthContext();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult CreateAdministrator()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdministratorAsync(AdicionarAdministradorRequestDTO adicionarAdministradorRequestDTO)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await _serviceAuth.AdicionarAdministrador(adicionarAdministradorRequestDTO);
                return RedirectToAction(nameof(Login));
            }, "Falha ao Adicionar Administrador!");

        }
        public IActionResult CreateUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario(AdicionarUsuarioRequestDTO adicionarUsuario)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await _serviceAuth.AdicionarUsuario(adicionarUsuario);
                return RedirectToAction(nameof(Login));

            }, "Falha ao Adicionar Usuario!");

        }

        protected ActionResult MiddlewareDeRetorno(Func<ActionResult> retorno)
        {
            try
            {
                var res = retorno();

                return res;
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        protected ActionResult MiddlewareDeRetorno(Func<ActionResult> retorno, string erroMessage)
        {
            try
            {
                var res = retorno();

                return res;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Usuário ou senha inválidos.";
                return View();
            }
        }
        protected async Task<ActionResult> MiddlewareDeRetorno(Func<Task<ActionResult>> retorno)
        {
            try
            {
                var res = await retorno();

                return res;
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        protected async Task<ActionResult> MiddlewareDeRetorno(Func<Task<ActionResult>> retorno, string erroMessage)
        {
            try
            {
                var res = await retorno();

                return res;
            }
            catch (Exception ex)
            {
                ViewBag.Error = erroMessage;
                return View();
            }
        }
    }
}
