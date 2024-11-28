using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UMBIT.ToDo.BuildingBlocksc.ASPNet.Basicos.Exececoes;
using UMBIT.ToDo.Core.Basicos.Excecoes;
using UMBIT.ToDo.Core.Basicos.Notificacoes;
using static UMBIT.ToDo.Web.Bootstrapper.ContextConfigurate;

namespace UMBIT.ToDo.Core.API.Controllers
{
    public abstract class ASPBaseController : Controller
    {
        protected AuthSessionContext _authSessionContext { get; set; }
        protected ASPBaseController(AuthSessionContext authSessionContext)
        {
            _authSessionContext = authSessionContext;
        }

        protected ActionResult MiddlewareDeRetorno(Func<ActionResult> retorno)
        {
            try
            {
                var res = retorno();

                return res;
            }
            catch (ExcecaoServicoExterno ex)
            {
                TempData["Notifications"] = ex.APIReposta?.Erros;

                return base.View();
            }
            catch (ExcecaoBasicaUMBIT ex)
            {
                TempData["Notifications"] = new List<NotificacaoPadrao>
                {
                    new NotificacaoPadrao { Titulo ="Erro Generico!", Mensagem = ex.Message },
                };
                return base.View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        protected ActionResult MiddlewareDeRetorno(Func<ActionResult> retorno, string erroMessage)
        {
            try
            {
                var res = retorno();

                return res;
            }
            catch (ExcecaoServicoExterno ex)
            {
                TempData["Notifications"] = ex.APIReposta?.Erros;

                return base.View();
            }
            catch (ExcecaoBasicaUMBIT ex)
            {
                TempData["Notifications"] = new List<NotificacaoPadrao>
                {
                    new NotificacaoPadrao { Titulo ="Erro Generico!", Mensagem = ex.Message },
                };
                return base.View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        protected async Task<ActionResult> MiddlewareDeRetorno(Func<Task<ActionResult>> retorno)
        {
            try
            {
                var res = await retorno();

                return res;
            }
            catch (ExcecaoServicoExterno ex)
            {
                TempData["Notifications"] = JsonSerializer.Serialize(ex.APIReposta?.Erros.ToList());

                return RedirectToAction(RouteData.Values["action"].ToString(), RouteData.Values);
            }
            catch (ExcecaoBasicaUMBIT ex)
            {
                TempData["Notifications"] = new List<NotificacaoPadrao>
                {
                    new NotificacaoPadrao { Titulo ="Erro Generico!", Mensagem = ex.Message },
                };
                return base.View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        protected async Task<ActionResult> MiddlewareDeRetorno(Func<Task<ActionResult>> retorno, string erroMessage)
        {
            try
            {
                var res = await retorno();

                return res;
            }
            catch (ExcecaoServicoExterno ex)
            {
                TempData["Notifications"] = ex.APIReposta?.Erros;

                return base.View();
            }
            catch (ExcecaoBasicaUMBIT ex)
            {
                TempData["Notifications"] = new List<NotificacaoPadrao>
                {
                    new NotificacaoPadrao { Titulo ="Erro!", Mensagem = ex.Message },
                };
                return base.View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }

}
