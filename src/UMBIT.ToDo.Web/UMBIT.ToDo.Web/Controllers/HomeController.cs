using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UMBIT.ToDo.Web.Basicos.Enumerador;
using UMBIT.ToDo.Web.Basicos.Extensores;
using UMBIT.ToDo.Web.Models;
using UMBIT.ToDo.Web.services;

namespace UMBIT.ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServicoDeToDo ServicoDeToDo;
        public HomeController(ILogger<HomeController> logger, IServicoDeToDo servicoDeToDo)
        {
            _logger = logger;
            this.ServicoDeToDo = servicoDeToDo;
        }

        public IActionResult Index(int status = -1, Guid? idList = null)
        {
            return MiddlewareDeRetorno(() =>
            {
                var result = this.ServicoDeToDo.ObtenhaItens().Result
                                               .Where(t => (status != -1 ? t.Status == status : true) && (idList != null ? t.IdToDoList == idList : true));

                var lists = this.ServicoDeToDo.ObtenhaLists().Result;

                ViewBag.Status = status;
                ViewBag.IdList = idList;
                ViewBag.Lists = lists;


                ViewBag.PieData = GerePieData(result);

                return View(result);
            });
        }

        public IActionResult AddTask()
        {
            var lists = this.ServicoDeToDo.ObtenhaLists().Result;
            ViewBag.Lists = lists;
            return View(new TaskDTO());
        }
        [HttpPost]
        public IActionResult AddTask(TaskDTO taskDTO)
        {
            return MiddlewareDeRetorno(() =>
            {

                if (!ModelState.IsValid)
                    return View(taskDTO);


                this.ServicoDeToDo.AdicioneItem(taskDTO).Wait();

                return RedirectToAction("Index");

            });

        }

        public IActionResult EditTask(Guid id)
        {
            return MiddlewareDeRetorno(() =>
            {
                var lists = this.ServicoDeToDo.ObtenhaLists().Result;
                ViewBag.Lists = lists;

                var result = this.ServicoDeToDo.ObtenhaItem(id).Result;
                return View(result);
            });
        }
        [HttpPost]
        public IActionResult EditTask(TaskDTO taskDTO)
        {
            return MiddlewareDeRetorno(() =>
            {
                if (!ModelState.IsValid)
                    return View(taskDTO);

                this.ServicoDeToDo.EditItem(taskDTO).Wait();

                return RedirectToAction("Index");

            });
        }

        public IActionResult DeleteTask(Guid id)
        {
            return MiddlewareDeRetorno(() =>
            {
                this.ServicoDeToDo.DeleteItem(id).Wait();

                return RedirectToAction("Index");
            });
        }
        public IActionResult DeleteList(Guid id)
        {
            return MiddlewareDeRetorno(() =>
            {
                this.ServicoDeToDo.DeleteList(id).Wait();

                return RedirectToAction("Index");
            });
        }

        public IActionResult AddList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddList(ListTaskDTO list)
        {
            return MiddlewareDeRetorno(() =>
            {
                this.ServicoDeToDo.AdicioneList(list).Wait();
                return RedirectToAction("Index");
            });
        }


        public IActionResult EditList(Guid id)
        {
            return MiddlewareDeRetorno(() =>
            {
                var result = this.ServicoDeToDo.ObtenhaList(id).Result;
                return View(result);
            });
        }

        [HttpPost]
        public IActionResult EditList(ListTaskDTO listTaskDTO)
        {
            return MiddlewareDeRetorno(() =>
            {
                if (!ModelState.IsValid)
                    return View(listTaskDTO);

                this.ServicoDeToDo.EditList(listTaskDTO).Wait();

                return RedirectToAction("Index");

            });
        }


        public IActionResult AltereStatus([FromQuery] int status, [FromQuery] Guid id)
        {
            return MiddlewareDeRetorno(() =>
            {

                var item = this.ServicoDeToDo.ObtenhaItem(id).Result;
                item.Status = status;
                this.ServicoDeToDo.EditItem(item).Wait();

                return RedirectToAction("Index");

            });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GerePieData(IEnumerable<TaskDTO>? taskDTOs)
        {
            string resultItems = "[ITEMS]";
            string items = "";

            if (taskDTOs != null && taskDTOs.Any())
            {
                var list = new List<string>();
                var groups = taskDTOs.GroupBy(t => t.Status);

                foreach (var group in groups)
                {
                    list.Add( $"{{ value: {group.Count()}, name: '{EnumeradorStatus.Parse<EnumeradorStatus>(group.Key.ToString()).GetStatus()}' }} ");
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

    }
}
