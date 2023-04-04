using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;
using ProyectoPoliza.Servicios.Implementacion;

namespace ProyectoPoliza.Controllers
{
    [Authorize]
    public class PolizasController : Controller
    {
        private readonly IGenericService<Poliza> _polizaService;
        public PolizasController(IGenericService<Poliza> empleadoService)
        {
            _polizaService = empleadoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaPoliza()
        {
            List<Poliza> _lista = await _polizaService.List();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPoliza([FromBody] Poliza model)
        {
            bool _resultado = await _polizaService.Save(model);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarPoliza([FromBody] Poliza model)
        {
            bool _resultado = await _polizaService.Edit(model);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
            }
        }

        [HttpPut]

        public async Task<IActionResult> EliminarPoliza(int idPoliza)
        {
            bool _resultado = await _polizaService.Delete(idPoliza);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
            }
        }
    }
}
