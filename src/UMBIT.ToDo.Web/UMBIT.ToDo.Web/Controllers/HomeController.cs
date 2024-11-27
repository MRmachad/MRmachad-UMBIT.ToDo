using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UMBIT.Nexus.Auth.Contrato;
using UMBIT.ToDo.Web.Basicos.Enumerador;
using UMBIT.ToDo.Web.Basicos.Extensores;
using UMBIT.ToDo.Web.Models;
using UMBIT.ToDo.Web.services;
using static UMBIT.ToDo.Web.Bootstrapper.AuthConfigurate;

namespace UMBIT.ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicoToDo _servicoDeToDo;
        private readonly ILogger<HomeController> _logger;
        private readonly AuthSessionContext _authSessionContext;
        public HomeController(ILogger<HomeController> logger, IServicoToDo servicoDeToDo, AuthSessionContext authSessionContext)
        {
            _logger = logger;
            _servicoDeToDo = servicoDeToDo;
            _authSessionContext = authSessionContext;
        }

        public IActionResult Index(int status = -1, Guid? idList = null)
        {
            return MiddlewareDeRetorno(() =>
            {
                if (!_authSessionContext.EhAutenticado)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var idPaia = Guid.NewGuid();

                var tasks = new List<TarefaDTO>
        {
            new TarefaDTO
            {
                Id = Guid.NewGuid(),
                Index = 1,
                Nome = "Task 1",
                Descricao = "Descrição da Task 1",
                Status = 1,
                DataInicio = DateTime.Now.AddDays(-1),
                DataFim = DateTime.Now,
            },
            new TarefaDTO
            {
                Id = Guid.NewGuid(),
                IdToDoList = idPaia,
                Index = 1,
                Nome = "Task 1",
                Descricao = "Descrição da Task 1",
                Status = 1,
                DataInicio = DateTime.Now.AddDays(-1),
                DataFim = DateTime.Now,
                ListaPai = new ListaDTO { Id = idPaia, Nome = "Lista A" }
            },
            new TarefaDTO
            {
                Id = Guid.NewGuid(),
                IdToDoList = idPaia,
                Index = 2,
                Nome = "Task 2",
                Descricao = "Descrição da Task 2",
                Status = 2,
                DataInicio = DateTime.Now.AddDays(-2),
                DataFim = DateTime.Now.AddDays(1),
                ListaPai = new ListaDTO { Id =idPaia, Nome = "Lista A" }
            },
            new TarefaDTO
            {
                Id = Guid.NewGuid(),
                IdToDoList = Guid.NewGuid(),
                Index = 3,
                Nome = "Task 3",
                Descricao = "Descrição da Task 3",
                Status = 3,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(2),
                ListaPai = new ListaDTO { Id = Guid.NewGuid(), Nome = "Lista B" }
            }
        };
                return View(tasks);
            });
        }

        public async Task<IActionResult> ListaTarefa(int? status = null, Guid? idList = null)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                var result = (await this._servicoDeToDo.ObtenhaItens(status, idList));

                var lists = await this._servicoDeToDo.ObtenhaLists();

                ViewBag.Status = status;
                ViewBag.IdList = idList;
                ViewBag.Lists = lists;


                ViewBag.PieData = GerePieData(result);

                return View(result);
            });
        }

        public async Task<IActionResult> AddTask()
        {
            var lists = await this._servicoDeToDo.ObtenhaLists();
            ViewBag.Lists = lists;
            return View(new AdicionarTarefaRequest());
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(AdicionarTarefaRequest taskDTO)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                if (!ModelState.IsValid)
                    return View(taskDTO);

                await this._servicoDeToDo.AdicioneItem(taskDTO);

                return RedirectToAction("ListaTarefa");

            });

        }

        public async Task<IActionResult> EditTask(Guid id)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                var lists = await this._servicoDeToDo.ObtenhaLists();
                ViewBag.Lists = lists;

                var result = await this._servicoDeToDo.ObtenhaItem(id);
                return View(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(AtualizarTarefaRequest taskDTO)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                if (!ModelState.IsValid)
                    return View(taskDTO);

                await this._servicoDeToDo.EditItem(taskDTO);

                return RedirectToAction("ListaTarefa");

            });
        }

        public async Task<IActionResult> DeleteTask(Guid id)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await this._servicoDeToDo.DeleteItem(id);

                return RedirectToAction("ListaTarefa");
            });
        }
        public async Task<IActionResult> DeleteList(Guid id)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await this._servicoDeToDo.DeleteList(id);

                return RedirectToAction("ListaTarefa");
            });
        }

        public IActionResult AddList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddList(AdicionarListaRequest list)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                await this._servicoDeToDo.AdicioneList(list);
                return RedirectToAction("ListaTarefa");
            });
        }

        public async Task<IActionResult> EditList(Guid id)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                var result = await this._servicoDeToDo.ObtenhaList(id);
                return View(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditList(AtualizarListaRequest listTaskDTO)
        {
            return await MiddlewareDeRetorno(async () =>
            {
                if (!ModelState.IsValid)
                    return View(listTaskDTO);

                await this._servicoDeToDo.EditList(listTaskDTO);

                return RedirectToAction("ListaTarefa");

            });
        }

        public async Task<IActionResult> AltereStatus([FromQuery] int status, [FromQuery] Guid id)
        {
            return await MiddlewareDeRetorno(async () =>
            {

                var item = this._servicoDeToDo.ObtenhaItem(id).Result;
                item.Status = status;
                await this._servicoDeToDo.EditItem(new AtualizarTarefaRequest()
                {
                    Id = id,
                    Nome = item.Nome,
                    Index = item.Index,
                    Status = item.Status,
                    DataFim = item.DataFim,
                    DataInicio = item.DataInicio,
                    Descricao = item.Descricao, 
                    IdToDoList = item.IdToDoList,    
                });

                return RedirectToAction("ListaTarefa");

            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GerePieData(IEnumerable<TarefaDTO>? taskDTOs)
        {
            string resultItems = "[ITEMS]";
            string items = "";

            if (taskDTOs != null && taskDTOs.Any())
            {
                var list = new List<string>();
                var groups = taskDTOs.GroupBy(t => t.Status);

                foreach (var group in groups)
                {
                    list.Add($"{{ value: {group.Count()}, name: '{EnumeradorStatus.Parse<EnumeradorStatus>(group.Key.ToString()).GetStatus()}' }} ");
                }

                items = String.Join(",", list);
            }

            return resultItems.Replace("ITEMS", items);
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
