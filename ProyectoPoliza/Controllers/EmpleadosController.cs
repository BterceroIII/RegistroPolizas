using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;

namespace ProyectoPoliza.Controllers
{
    [Authorize]
    public class EmpleadosController : Controller
    {
        private readonly IGenericService<Empleado> _empleadoService;
        public EmpleadosController(IGenericService<Empleado> empleadoService)
        {
            _empleadoService = empleadoService;
        }
    
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaEmpleado()
        {
            List<Empleado> _lista = await _empleadoService.List();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarEmpleado([FromBody] Empleado model)
        {
            bool _resultado = await _empleadoService.Save(model);

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
        public async Task<IActionResult> EditarEmpleado([FromBody] Empleado model)
        {
            bool _resultado = await _empleadoService.Edit(model);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new{ valor = _resultado, msg = "ok"});
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
            }
        }

        [HttpDelete]

        public async Task<IActionResult> EliminarEmpleado(int idEmopleado)
        {
            bool _resultado = await _empleadoService.Delete(idEmopleado);

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
