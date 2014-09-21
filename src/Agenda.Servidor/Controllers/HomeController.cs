using System.Web.Mvc;
using Agenda.Servidor.Model;
using Restfulie.Server;
using Restfulie.Server.Results;

namespace Agenda.Servidor.Controllers
{
    [ActAsRestfulie]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var recursos = new Recursos { Message = "Inicio da api", Contatos = Rotas.Contatos() };
            return new OK(recursos);
        }
    }
}