
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;
using ProyectoPoliza.Servicios.Implementacion;

namespace ProyectoPoliza.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly IGenericService<Cliente> _clienteService;
        public ClientesController(IGenericService<Cliente> clienteService)
        {
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaClientes()
        {
            List<Cliente> _lista = await _clienteService.List();

            //IQueryable<Cliente> queryContactoSQL = await _ClienteService.ObtenerTodos();

            //List<ClienteVM> _lista = queryContactoSQL
            //                                         .Select(c => new ClienteVM()
            //                                         {
            //                                             IdCliente = c.IdCliente,
            //                                             Nombre = c.Nombre,
            //                                             Cedula = c.Cedula,
            //                                             Telefono = c.Telefono,
            //                                             Cargo = c.Cargo,
            //                                             Eliminado = c.Eliminado.Value
            //                                         }).ToList();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCliente([FromBody] Cliente model)
        {
            bool _resultado = await _clienteService.Save(model);

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
        public async Task<IActionResult> EditarCliente([FromBody] Cliente model)
        {
            bool _resultado = await _clienteService.Edit(model);

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

        public async Task<IActionResult> EliminarCliente(int idCliente)
        {
            bool _resultado = await _clienteService.Delete(idCliente);

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
