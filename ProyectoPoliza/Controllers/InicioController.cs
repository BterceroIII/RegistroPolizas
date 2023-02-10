using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using ProyectoPoliza.Models;
using ProyectoPoliza.Recursos;
using ProyectoPoliza.Servicios.Contrato;

using System.Security.Claims;

namespace ProyectoPoliza.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly GestionSegurosSaContext _gestionSegurosSaContext;

        public InicioController(IUsuarioService usuarioService, GestionSegurosSaContext gestionSegurosSaContext)
        {
            _usuarioService = usuarioService;   
            _gestionSegurosSaContext = gestionSegurosSaContext;   
        }

        public IActionResult Registrarse() 
        {
            return View();        
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.Contraseña = Utilidades.EncriptarClave(modelo.Contraseña);
            modelo.Eliminado = 0;
            Usuario usuario_creado = await _usuarioService.SaveUsuario(modelo);
            if (usuario_creado.IdUsuario > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";


            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> IniciarSesion(string correo, string clave, int estado)
        {
            Usuario usuario_encontrado = await _usuarioService.GetUsuario(correo,Utilidades.EncriptarClave(clave), estado);

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontro coincidencias";
                return View();
            }
            else if (usuario_encontrado.Eliminado != 0)
            {
                ViewData["Mensaje"] = "El Usuario esta inactivo";
                return View();
            }
           

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.oEmpleado.Nombre)
            };

            ClaimsIdentity claimsIdentity= new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh =true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            return RedirectToAction("Index", "Home");
        }
    }
}
