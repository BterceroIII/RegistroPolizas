using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProyectoPoliza.Models;
using ProyectoPoliza.Models.ViewModels;
using ProyectoPoliza.Servicios.Contrato;
using System.Diagnostics.Contracts;

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
        public async Task<IActionResult> ListaEmpleados()
        {
            List<Empleado> _lista = await _empleadoService.List();

            //IQueryable<Empleado> queryContactoSQL = await _empleadoService.ObtenerTodos();

            //List<EmpleadoVM> _lista = queryContactoSQL
            //                                         .Select(c => new EmpleadoVM()
            //                                         {
            //                                             IdEmpleado = c.IdEmpleado,
            //                                             Nombre = c.Nombre,
            //                                             Cedula = c.Cedula,
            //                                             Telefono = c.Telefono,
            //                                             Cargo = c.Cargo,
            //                                             Eliminado = c.Eliminado.Value
            //                                         }).ToList();

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

        [HttpPut]

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
