using Microsoft.AspNetCore.Mvc;

namespace ProyectoPoliza.Controllers
{
    public class EmpleadosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
