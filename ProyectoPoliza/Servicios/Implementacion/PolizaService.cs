using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoPoliza.Servicios.Implementacion
{
    public class PolizaService : IGenericService<Poliza>
    {
        private readonly string _cadenaSQL = "";

        public PolizaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }
        public async Task<List<Poliza>> List()
        {
            List<Poliza> _lista = new List<Poliza>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Mostrar_Poliza", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Poliza
                        {
                            IdPoliza = Convert.ToInt32(dr["idPoliza"]),
                            Aseguradora = dr["Aseguradora"].ToString(),
                            Tipo = dr["Tipo"].ToString(),
                            Codigo = dr["Codigo"].ToString(),
                        });
                    }

                }

            }

            return _lista;
        }

        public async Task<bool> Save(Poliza model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Poliza", conexion);
                cmd.Parameters.AddWithValue("codigo", model.Codigo);
                cmd.Parameters.AddWithValue("aseguradora", model.Aseguradora);
                cmd.Parameters.AddWithValue("tipo", model.Tipo);

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

        public async Task<bool> Edit(Poliza model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Poliza", conexion);
                cmd.Parameters.AddWithValue("idPoliza", model.IdPoliza);
                cmd.Parameters.AddWithValue("codigo", model.Codigo);
                cmd.Parameters.AddWithValue("aseguradora", model.Aseguradora);
                cmd.Parameters.AddWithValue("tipo", model.Tipo);

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

        public async Task<bool> Delete(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Eliminar_Poliza", conexion);
                cmd.Parameters.AddWithValue("idPoliza", id);
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
