using Microsoft.EntityFrameworkCore;
using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;

namespace ProyectoPoliza.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly GestionSegurosSaContext _SeguroContext;

        public UsuarioService(GestionSegurosSaContext seguroContext)
        {
            _SeguroContext = seguroContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave, int estado)
        {
            Usuario usuario_encontrado = await _SeguroContext.Usuarios.Where(u => u.Correo == correo && u.Contraseña == clave && estado == 0).Include(c => c.oEmpleado)
                .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _SeguroContext.Usuarios.Add(modelo);
            await _SeguroContext.SaveChangesAsync();

            return modelo;
        }
    }
}
