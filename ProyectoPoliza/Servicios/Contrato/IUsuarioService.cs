using ProyectoPoliza.Models;

namespace ProyectoPoliza.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(String correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);
    }
}
