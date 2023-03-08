using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoPoliza.Controllers
{
    [Authorize]
    public class DetallePolizasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
