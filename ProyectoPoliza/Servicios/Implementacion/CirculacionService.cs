using Microsoft.Data.SqlClient;
using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;
using System.Data;

namespace ProyectoPoliza.Servicios.Implementacion
{
    public class CirculacionService : IGenericService<Vehiculo>
    {
        private readonly string _cadenaSQL = "";

        public CirculacionService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<List<Vehiculo>> List()
        {
            List<Vehiculo> _lista = new List<Vehiculo>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Mostrar_Vehiculo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Vehiculo()
                        {
                            IdVehiculo = Convert.ToInt32(dr["idVehiculo"]),
                            refCliente = new Cliente()
                            {
                                IdCliente = Convert.ToInt32(dr["idCliente"]),
                                NombreCliente = dr["NombreCliente"].ToString()
                            },
                            Tipo = dr["idVehiculo"].ToString(),
                            NoCirculacion = dr["NoCirculacion"].ToString(),
                            NoPlaca = dr["NoPlaca"].ToString(),
                            Marca = dr["Marca"].ToString(),
                            Modelo = dr["Modelo"].ToString(),
                            NoMotor = dr["NoMotor"].ToString(),
                            NoChasis = dr["NoChasis"].ToString(),
                            Uso = dr["Uso"].ToString(),
                            Eliminado = Convert.ToInt32(dr["Eliminado"]),
                        }); 
                    }
                }
            }
                
            return _lista;
        }

        public async Task<bool> Save(Vehiculo model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Vehiculo", conexion);
                cmd.Parameters.AddWithValue("idCliente", model.refCliente.NombreCliente);
                cmd.Parameters.AddWithValue("Tipo", model.Tipo);
                cmd.Parameters.AddWithValue("NoCirculacion", model.NoCirculacion);
                cmd.Parameters.AddWithValue("NoPlaca", model.NoPlaca);
                cmd.Parameters.AddWithValue("Marca", model.Marca);
                cmd.Parameters.AddWithValue("Modelo", model.Modelo);
                cmd.Parameters.AddWithValue("NoMotor", model.NoMotor);
                cmd.Parameters.AddWithValue("NoChasis", model.NoChasis);
                cmd.Parameters.AddWithValue("Uso", model.Uso);
                cmd.Parameters.AddWithValue("Año", model.Año);

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

        public async Task<bool> Edit(Vehiculo model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Modificar_Vehiculo", conexion);
                cmd.Parameters.AddWithValue("idCliente", model.refCliente.NombreCliente);
                cmd.Parameters.AddWithValue("Tipo", model.Tipo);
                cmd.Parameters.AddWithValue("NoCirculacion", model.NoCirculacion);
                cmd.Parameters.AddWithValue("NoPlaca", model.NoPlaca);
                cmd.Parameters.AddWithValue("Marca", model.Marca);
                cmd.Parameters.AddWithValue("Modelo", model.Modelo);
                cmd.Parameters.AddWithValue("NoMotor", model.NoMotor);
                cmd.Parameters.AddWithValue("NoChasis", model.NoChasis);
                cmd.Parameters.AddWithValue("Uso", model.Uso);
                cmd.Parameters.AddWithValue("Año", model.Año);

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
                SqlCommand cmd = new SqlCommand("Eliminar_Vehiculo", conexion);
                cmd.Parameters.AddWithValue("idVehiculo", id);
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


