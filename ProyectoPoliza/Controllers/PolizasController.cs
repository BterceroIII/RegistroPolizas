using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoPoliza.Controllers
{
    [Authorize]
    public class PolizasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
