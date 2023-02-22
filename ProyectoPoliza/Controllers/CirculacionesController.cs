using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;
using ProyectoPoliza.Servicios.Implementacion;

namespace ProyectoPoliza.Controllers
{
    [Authorize]
    public class CirculacionesController : Controller
    {
        private readonly IGenericService<Vehiculo> _VehiculoService;

        public CirculacionesController(IGenericService<Vehiculo> vehiculoService)
        {
            _VehiculoService = vehiculoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaVehiculos()
        {
            List<Vehiculo> _lista = await _VehiculoService.List();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarVehiculo([FromBody] Vehiculo model)
        {
            bool _resultado = await _VehiculoService.Save(model);

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
        public async Task<IActionResult> EditarVehiculo([FromBody] Vehiculo model)
        {
            bool _resultado = await _VehiculoService.Edit(model);

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
        public async Task<IActionResult> EliminarVehiculo(int idVehiculo)
        {
            bool _resultado = await _VehiculoService.Delete(idVehiculo);

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
