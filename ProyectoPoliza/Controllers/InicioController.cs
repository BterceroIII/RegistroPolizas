﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using ProyectoPoliza.Models;
using ProyectoPoliza.Recursos;
using ProyectoPoliza.Servicios.Contrato;

using System.Security.Claims;

namespace ProyectoPoliza.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public InicioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;   
        }

        public IActionResult Registrarse() 
        {
            return View();        
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.Contraseña = Utilidades.EncriptarClave(modelo.Contraseña);
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

        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            Usuario usuario_encontrado = await _usuarioService.GetUsuario(correo,clave /*Utilidades.EncriptarClave(clave)*/);

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontro coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.Correo)
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
