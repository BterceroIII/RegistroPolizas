using Microsoft.AspNetCore.Mvc;

namespace ProyectoPoliza.Controllers
{
    public class PolizasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
