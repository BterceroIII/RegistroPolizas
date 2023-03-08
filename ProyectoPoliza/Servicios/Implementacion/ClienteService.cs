using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoPoliza.Servicios.Implementacion
{
    public class ClienteService : IGenericService<Cliente>
    {
        private readonly string _cadenaSQL = "";

        public ClienteService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<List<Cliente>> List()
        {
            List<Cliente> _lista = new List<Cliente>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Mostrar_Clientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Cliente()
                        {
                            IdCliente = Convert.ToInt32(dr["idCliente"]),
                            NombreCliente = dr["NombreCliente"].ToString(),
                            TipoDocIdentidad = dr["TipoDocIdentidad"].ToString(),
                            NoDocIdentidad = dr["NoDocIdentidad"].ToString(),
                            Nacionalidad = dr["Nacionalidad"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            //Eliminado = Convert.ToInt32(dr["Eliminado"]),
                        });
                    }
                }
            }

            return _lista;
        }

        public async Task<bool> Save(Cliente model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Cliente", conexion);
                cmd.Parameters.AddWithValue("Nombre", model.NombreCliente);
                cmd.Parameters.AddWithValue("TipoDocIdentidad", model.TipoDocIdentidad);
                cmd.Parameters.AddWithValue("NoDocIdentidad", model.NoDocIdentidad);
                cmd.Parameters.AddWithValue("Nacionalidad", model.Nacionalidad);
                cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                cmd.Parameters.AddWithValue("Direccion", model.Direccion);
                cmd.Parameters.AddWithValue("Correo", model.Correo);

                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<bool> Edit(Cliente model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Modificar_Cliente", conexion);
                    cmd.Parameters.AddWithValue("idCliente", model.IdCliente);
                    cmd.Parameters.AddWithValue("Nombre", model.NombreCliente);
                    cmd.Parameters.AddWithValue("TipoDocIdentidad", model.TipoDocIdentidad);
                    cmd.Parameters.AddWithValue("NoDocIdentidad", model.NoDocIdentidad);
                    cmd.Parameters.AddWithValue("Nacionalidad", model.Nacionalidad);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", model.Direccion);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);

                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    if (filas_afectadas > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                

            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Eliminar_Cliente", conexion);
                cmd.Parameters.AddWithValue("idcliente", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
