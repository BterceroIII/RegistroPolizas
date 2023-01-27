using Microsoft.AspNetCore.Mvc;

namespace ProyectoPoliza.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
