using Microsoft.AspNetCore.Mvc;

namespace ProyectoPoliza.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
