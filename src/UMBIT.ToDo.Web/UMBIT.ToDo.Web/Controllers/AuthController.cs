using Microsoft.AspNetCore.Mvc;
using UMBIT.ToDo.Core.API.Controllers;
using UMBIT.ToDo.Web.Models;
using UMBIT.ToDo.Web.services;

namespace UMBIT.ToDo.Web.Controllers
{
    public class AuthController : Controller
    {
        private IServicoAuth _serviceAuth { get; set; }
        public AuthController(IServicoAuth serviceAuth)
        {
            _serviceAuth = serviceAuth;
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
        public async Task<IActionResult> Login(string username, string password)
        {
            if (!(await _serviceAuth.CheckAuth()).Configured)
            {
                return RedirectToAction(nameof(CreateAdministrator));
            }

            return View();
        }
        public IActionResult CreateAdministrator()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdministratorAsync(AdicionarAdministradorRequestDTO adicionarAdministradorRequestDTO)
        {
            return await  MiddlewareDeRetorno(async () =>
            {
                await _serviceAuth.AdicionarAdministrador(adicionarAdministradorRequestDTO);
                return RedirectToAction(nameof(Login));
            },"Falha ao Adicionar Administrador!");

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
                var res =  await retorno();

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
