using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using UMBIT.Nexus.Auth.Contrato;
using UMBIT.ToDo.Web.services;
using static UMBIT.ToDo.Web.Bootstrapper.AuthConfigurate;

namespace UMBIT.ToDo.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private IServicoUser _serviceUser { get; set; }
        private AuthSessionContext _authSessionContext { get; set; }
        public UsuariosController(IServicoUser serviceUser, AuthSessionContext authSessionContext)
        {
            _serviceUser = serviceUser;
            _authSessionContext = authSessionContext;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _serviceUser.GetUsuarios();

            return View(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
             await _serviceUser.RemoverUsuario(id);

            return Json(new { success = true, message = "Usuário removido com sucesso." });
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
