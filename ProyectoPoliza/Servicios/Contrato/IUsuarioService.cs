using ProyectoPoliza.Models;

namespace ProyectoPoliza.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(String correo, string clave, int estado);
        Task<Usuario> SaveUsuario(Usuario modelo);
    }
}
